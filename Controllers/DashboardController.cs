using RegisterLoginMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RegisterLoginMVC.Controllers
{

    public class DashboardController : Controller
 
    {
        cab_bookingEntities db = new cab_bookingEntities();

        // GET: Dashboard

        
        public ActionResult Index(int userid)
        {
            var u = db.Users.Find(userid);
            return View(u);
        }



        public ActionResult CarDetails()
        {
           
            return View(db.CarModels);
        }
        public ActionResult BookNow(int CarId,string CarName,int Seats)
        {
            CarModel model = new CarModel();
            ViewBag.CarId = CarId;
            ViewBag.CarName=CarName;
            ViewBag.Seats = Seats;
            return View();
        }
        [HttpPost]
        public ActionResult BookNow(BookingList bookinglist)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    
                    BookingList book = new BookingList();
                    book.UserId = bookinglist.UserId;
                    book.AdminId = bookinglist.AdminId;
                    book.CarId = bookinglist.CarId;
                    book.CARName = bookinglist.CARName;
                    book.Seats = bookinglist.Seats;
                    book.PickUp = bookinglist.PickUp;
                    book.Drop = bookinglist.Drop;
                    book.PickUpDate = bookinglist.PickUpDate;
                    if (book.PickUp=="Hyderabad" && book.Drop == "Pune")
                    {
                        book.Fare = 1500;
                        
                        
                    }
                    else
                    {
                        book.Fare = 3000;
                    }
                    
                    book.PickUPTime = bookinglist.PickUPTime;
                    book.Status = "ToBeConfirmed";
                    db.BookingLists.Add(book);
                    db.SaveChanges();
                    bookinglist=new BookingList();
                    return RedirectToAction("Index", "Dashboard",new{ book.UserId});
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        public ActionResult MyBookingList(int UserId)
        {
            User user = new User();
            List<BookingList> bookings = new List<BookingList>();
            bookings = db.BookingLists.Where(b => b.UserId == UserId).ToList();
            return View(bookings);

        }
        /*
        public ActionResult MyBookingList()
        {
            
           // Get the current user's ID (you need to implement this logic)
          //  bookings = db.BookingLists.Where(b => b.UserId == UserId).ToList();
            return View(db.BookingLists);
            /* User user = db.Users.FirstOrDefault(u => u.UserId == UserId);
             if (user != null)
             {
                 List<BookingList> bookings = db.BookingLists.Where(b => b.UserId == UserId).ToList();
                 return View(bookings);
             }*/
            
        }
    }
    
