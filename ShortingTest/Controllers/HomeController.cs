using ShortingTest.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShortingTest.Controllers
{
     

    public class HomeController : Controller
    {
    

        public ActionResult Index()
        {
            Session["FixSorted"] = null;
            try {
                List<Data_REC> Listrows = new List<Data_REC>();


                var path = Server.MapPath(@"~/Content/source/unsorted-names-list.txt");
                using (StreamReader reader = new StreamReader(path))
                {
                    while (true)
                    {
                        string line = reader.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        else {
                         
                            Listrows.Add(new Data_REC { FullName = line });

                        }

                    }
                }





               
              var getNewData = Urutkan(Listrows);
                 
                List<Data_REC> ListrowsList = new List<Data_REC>();

                
                var path2 = Server.MapPath(@"~/Content/source/sorted-names-list.txt");

                using (StreamWriter writetext = new StreamWriter(path2))
                {
                    foreach (var writeRow in getNewData) {

                        writetext.WriteLine(writeRow.FullName);
                    }
                   
                }

                ViewData["Unsorted"] = Listrows;
                ViewData["Sorted"] = getNewData;

            }
            catch (Exception e)
            {
                ViewData["error"] = e.Message;
            }

            return View();
        }


        public List<Data_REC> Urutkan(List<Data_REC> arr)
        {

            var newList = new List<Data_Split_REC>();
            var OutList = new List<Data_REC>();

            var setData = arr.Select(x => x.FullName);

            foreach (string setRow in setData)
            {
                var pecah = setRow.Split(' ');
                int tot = pecah.Count();

                if (pecah.Count() ==2)
                {
                    newList.Add(
                        new Data_Split_REC {
                            Name1 = pecah[0].ToString(),
                            Name2 = pecah[1].ToString()
                        });
                }
                else if (pecah.Count() > 2 && pecah.Count() <= 3)
                {
                    newList.Add(new Data_Split_REC {
                        Name1 = pecah[0].ToString(),
                        Name2 = pecah[1].ToString(),
                        Name3 = pecah[2].ToString()
                    });

                }
                else if (pecah.Count() > 2 &&  pecah.Count() <= 4)
                {
                    newList.Add(new Data_Split_REC {
                        Name1 = pecah[0].ToString(),
                        Name2 = pecah[1].ToString(),
                        Name3 = pecah[2].ToString(),
                        Name4 = pecah[3].ToString()
                    });

                }
                else
                {
                    newList.Add(new Data_Split_REC { Name1 = pecah[0].ToString() });
                }


            }

            
          var result = newList.OrderBy(a => a.Name1)
                                .OrderBy(b=>b.Name2)
                                .OrderBy(c=>c.Name3)
                                .OrderBy(d=>d.Name4).ToList();



            foreach (var ends in result) {

                if (ends.Name2 == null)
                {
                    OutList.Add(new Data_REC { FullName = ends.Name1+" "});
                }
                else if (ends.Name3 == null)
                {
                    OutList.Add(new Data_REC { FullName = ends.Name1 +" "+ ends.Name2+" "});
                }
                else if (ends.Name4 == null)
                {
                    OutList.Add(new Data_REC { FullName = ends.Name1 +" "+ ends.Name2 +" "+ ends.Name3 });
                }
                else {
                    OutList.Add(new Data_REC { FullName = ends.Name1 +" "+ ends.Name2 +" "+ ends.Name3 +" "+ ends.Name4+" " });
                }
              
            }

            //Session["FixSorted"] = result;

            return OutList.ToList();
        }


      




    }
 
}