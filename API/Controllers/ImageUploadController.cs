using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Data.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ImageUploadController : ApiBaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly StoreContext _storeContext;

        public ImageUploadController(IHostingEnvironment hostingEnvironment , StoreContext storeContext)
        {
            _hostingEnvironment=hostingEnvironment;
            _storeContext = storeContext;
        }
        [HttpPost]
        public async Task<ActionResult<string>> UploadImage(){
           
           var files = HttpContext.Request.Form.Files;
           if (files!=null && files.Count >0)
           {
               foreach (var file in files)
               {
                   FileInfo fi = new FileInfo(file.FileName);
                   var newFileName = "Image_"+DateTime.Now.TimeOfDay.Milliseconds+fi.Extension;
                   var path = Path.Combine("", _hostingEnvironment.ContentRootPath+"/Images/"+newFileName);
                   using(var stream = new FileStream(path,FileMode.Create) ){
                       file.CopyTo(stream);
                   }
                   Image image = new Image();
                   image.Name = newFileName;
                   image.ImagePath = path;
                   image.InsertedOn=DateTime.Now;
                   await _storeContext.Images.AddAsync(image);
                   await _storeContext.SaveChangesAsync();

               }

               return Ok(new ApiResponse(201));
           }
           else{
               
               return BadRequest(new ApiResponse(400));
           }
       }   
       [HttpGet]
       public async Task<ActionResult<UploadImageDTO>> GetImages(){
           var result = await _storeContext.Images.ToListAsync();
           return Ok(new ApiResponse(200,result));
       }

       [HttpPost]
       [Route("download/{id}")]
       public async Task<ActionResult> Download(int id){
          var provider = new FileExtensionContentTypeProvider();
          var image = await _storeContext.Images.FindAsync(id);
          if (image == null)
          {
              return NotFound();
          }
         var file = Path.Combine("", _hostingEnvironment.ContentRootPath+"/Images/"+image.Name);
         string contentType;
         if (! provider.TryGetContentType(file,out contentType))
         {
             contentType = "application/octet-stream";
         }    
         byte[] fileBytes;
         if (System.IO.File.Exists(file))
         {
             fileBytes = System.IO.File.ReadAllBytes(file);
         }else{
             return NotFound();
         }
         return File(fileBytes,contentType,image.Name);

                     
       }
 
    }
}