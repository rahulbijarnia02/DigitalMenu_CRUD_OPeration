using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DIgitalMenu.Models;
using DIgitalMenu.Entities;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace DIgitalMenu.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

   private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
    public HomeController(ILogger<HomeController> logger, Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment)
    {
        _logger = logger;
        Environment = _environment;
    }
    
    
    public IActionResult Index()
    {
       return View();
         
    }
     public IActionResult ViewItemList()
    {
       using (var context=new EmployeeDBContext())
        {
            var employeeList=context.AddMenuDetails.FromSqlRaw("select id,Dishname,category,type,Image,Price,Quantity from AddmenuDetail").ToList();
             return View(employeeList);
        }
         
    }
   
     public IActionResult ItemDetailPage(int id)
{
    //handle your search stuff here...
         using (var context=new EmployeeDBContext())
        {
             var data=context.AddMenuDetails.FirstOrDefault(x=>x.Id==id);
             return View(data);
        }
    
}  
     public IActionResult AddMenu()
    {
        return View();
    }
    
    


 [HttpPost]
        public IActionResult AddDish(AddMenu DishDetail,IFormFile Image)
    {
       string wwwPath = this.Environment.WebRootPath;
        string contentPath = this.Environment.ContentRootPath;
        string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        List<string> uploadedFiles = new List<string>();
            string fileName = Path.GetFileName(Image.FileName);
                  using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                Image.CopyTo(stream);
                uploadedFiles.Add(fileName);
                ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
            }
       
        using (var context=new EmployeeDBContext())
        { 
            // AddMenuDetail employee=new AddMenuDetail();
            // employee.DishName =DishDetail.DishName;
            // employee.Category=DishDetail.Category;
            // employee.Type=DishDetail.Type;
            // employee.Image=fileName;
            // employee.Price=DishDetail.Price;
            // employee.Quantity=DishDetail.Quantity;
            // context.AddMenuDetails.Add(employee);
            // context.SaveChanges();
         SqlParameter dishname=new SqlParameter("@AddDishName",DishDetail.DishName);
         SqlParameter category=new SqlParameter("@Category",DishDetail.Category);
         SqlParameter type=new SqlParameter("@Type",DishDetail.Type);
         SqlParameter image=new SqlParameter("@Image",fileName);
         SqlParameter price=new SqlParameter("@Price",DishDetail.Price);
         SqlParameter quantity=new SqlParameter("@Quantity",DishDetail.Quantity);
         context.Database.ExecuteSqlRaw("usp_AddDish @AddDishName,@Category,@Type,@Image,@Price,@Quantity",dishname,category,type,image,price,quantity);
         
        }
        return RedirectToAction(actionName: "ViewItemList", controllerName: "Home");
      
    }




    public IActionResult Privacy()
    {
        return View();
    }
    [HttpGet]
 public IActionResult Editview(int id)
   {
        using (var context=new EmployeeDBContext())
        {
           
            var data=context.AddMenuDetails.FirstOrDefault(x=>x.Id==id);
            if(data!=null){
            var viewmodel=new Editview(){
            Id =data.Id,
            DishName =data.DishName,
            Category=data.Category,
            Type=data.Type,
            Image=data.Image,
            Price=data.Price,
            Quantity=data.Quantity,
            
            };
            return View(viewmodel);
        }
        return RedirectToAction(actionName: "ViewItemList", controllerName: "Home");
    }}
    [HttpPost]
    public IActionResult Editview(Editview model){
             using (var context=new EmployeeDBContext())
        {
       
            var data=context.AddMenuDetails.Find(model.Id);
            if(data!=null){

                data.DishName=model.DishName;
                data.Category=model.Category;
                data.Type=model.Type;
                data.Image=model.Image;
                data.Price=model.Price;
                data.Quantity=model.Quantity;
                context.SaveChanges();
                  return RedirectToAction(actionName: "ViewItemList", controllerName: "Home");
            }
               return RedirectToAction(actionName: "ViewItemList", controllerName: "Home");
    }}



    public IActionResult DeleteDish(int Id)
    {
        using (var context = new EmployeeDBContext())
        {
            var candidateRecord = context.AddMenuDetails.FirstOrDefault(x => x.Id == Id);
            if (candidateRecord != null)
            {
                context.AddMenuDetails.Remove(candidateRecord);
                context.SaveChanges();
            }
            return RedirectToAction(actionName: "ViewItemList", controllerName: "Home");
        }
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
