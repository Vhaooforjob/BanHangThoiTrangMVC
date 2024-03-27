using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanHangThoiTrangMVC.Models
{
    public class DbContextSingleton
    {
        private static ApplicationDbContext dbInstance;
        private static readonly object lockObject = new object();
        public static ApplicationDbContext GetInstance()
        {
            if (dbInstance == null)
            {
                lock (lockObject)
                {
                    if (dbInstance == null)
                    {
                        dbInstance = new ApplicationDbContext();
                    }
                }
            }
            return dbInstance;
        }
    }
}