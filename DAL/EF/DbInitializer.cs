namespace DAL.EF
{
    internal class DbInitializer
    {
        AppContext db;

        public DbInitializer(AppContext db)
        {
            this.db = db;
        }

        public void Init()
        { 
            //TimeLaps
            var timel = new List<string>()
            {
                "8:00","8:10","8:20","8:30","8:40","8:50",
                "9:00","9:10","9:20","9:30","9:40","9:50",
                "10:00","10:10","10:20","10:30","10:40","10:50",
                "11:00","11:10","11:20","11:30","11:40","11:50",
                "12:00","12:10","12:20","12:30","12:40","12:50",
                "13:00","13:10","13:20","13:30","13:40","13:50",
                "14:00","14:10","14:20","14:30","14:40","14:50",
                "15:00","15:10","15:20","15:30","15:40","15:50",
                "16:00","16:10","16:20","16:30","16:40","16:50",
                "17:00","17:10","17:20","17:30","17:40","17:50",
                "18:00","18:10","18:20","18:30","18:40","18:50"
            };


            //Schedule
            var date = DateTime.Now;
            var dateFull1 = new List<DateTime>();
            var dateFull2 = new List<DateTime>();

            for (int i = 0; i < 7; i++)
            {
                dateFull1.Add(date);
                dateFull1.Add(date.AddDays(1));
                dateFull1.Add(date.AddDays(2));
                dateFull1.Add(date.AddDays(3));
                dateFull1.Add(date.AddDays(4));
                date = date.AddDays(7);
            }
            date = DateTime.Now.AddDays(2);
            for (int i = 0; i < 7; i++)
            {
                dateFull2.Add(date);
                dateFull2.Add(date.AddDays(1));
                dateFull2.Add(date.AddDays(2));
                dateFull2.Add(date.AddDays(3));
                dateFull2.Add(date.AddDays(4));
                date = date.AddDays(7);
            }

            var schedule1 = new List<Schedule>();
            var schedule2 = new List<Schedule>();

            foreach (var item in dateFull1)
            {
                schedule1.Add(new() { Date = item });
            };
            db.Schedules.AddRange(schedule1);


            foreach (var item in dateFull2)
            {
                schedule2.Add(new() { Date = item});
            };
            db.Schedules.AddRange(schedule2);

            //Schedule
            dateFull1.Clear();
            dateFull2.Clear();

            date = DateTime.Now.AddDays(1);
            for (int i = 0; i < 7; i++)
            {
                dateFull1.Add(date);
                dateFull1.Add(date.AddDays(1));
                dateFull1.Add(date.AddDays(2));
                dateFull1.Add(date.AddDays(3));
                dateFull1.Add(date.AddDays(4));
                date = date.AddDays(7);
            }
            date = DateTime.Now.AddDays(3);
            for (int i = 0; i < 7; i++)
            {
                dateFull2.Add(date);
                dateFull2.Add(date.AddDays(1));
                dateFull2.Add(date.AddDays(2));
                dateFull2.Add(date.AddDays(3));
                dateFull2.Add(date.AddDays(4));
                date = date.AddDays(7);
            }

            var schedule3 = new List<Schedule>();
            var schedule4 = new List<Schedule>();

            foreach (var item in dateFull1)
            {
                schedule3.Add(new() { Date = item });
            };
            db.Schedules.AddRange(schedule3);


            foreach (var item in dateFull2)
            {
                schedule4.Add(new() { Date = item });
            };
            db.Schedules.AddRange(schedule4);

            //Service
            var ser1 = new List<Service> {
            new Service { Name = "Общий анализ мочи", Price = 3.7d, ServiceCategoryId = 1  },
            new Service { Name = "Забор крови из вены для всего спектра гемотологических исследований \"Общий анализ крови\"", Price = 3.39d, ServiceCategoryId = 1  },
            new Service { Name = "Взятие крови из пальца для определения глюкозы", Price = 1.28d, ServiceCategoryId = 1  },
            new Service { Name = "Взятие крови из пальца для всего спектра гематологических исследований в понятии \"общий анализ крови\" (детский)", Price = 1.84d, ServiceCategoryId = 1  }
            
            };
            var ser2 = new List<Service>
            {
            new Service { Name = "Мочевой пузырь", Price = 6.24d, ServiceCategoryId = 2 },
            new Service { Name = "Селезенка", Price = 6.24d, ServiceCategoryId = 2  },
            new Service { Name = "Щитовидная железа с лимфатическими поверхностными узлами", Price = 12.17d, ServiceCategoryId = 2  },
            new Service { Name = "Почки и надпочечники", Price = 12.17d, ServiceCategoryId = 2  }
            };

            db.Services.AddRange(ser1);
            db.Services.AddRange(ser2);


            // Doctor
            var doc1 = new Doctor { FullName = "Иванов Иван", Description = "22 лет опыта", Img = "../image/doctors/ivanov.png", Schedules = schedule1,ServiceCategoryId = 1};
            var doc2 = new Doctor { FullName = "Игнатьева Тамара", Description = "10 лет опыта", Img = "../image/doctors/ignateva.jpg", Schedules = schedule2 , ServiceCategoryId = 2 };
            var doc3 = new Doctor { FullName = "Александров Алексей", Description = "15 лет опыта", Img = "../image/doctors/alexandrov.jpg", Schedules = schedule3, ServiceCategoryId = 1 };
            var doc4 = new Doctor { FullName = "Кубарева Анна", Description = "7 лет опыта", Img = "../image/doctors/kubareva.jpg", Schedules = schedule4, ServiceCategoryId = 2 };



            var doclist1 = new List<Doctor> { doc1, doc3 };
            var doclist2 = new List<Doctor> { doc2, doc4 };

            //ServiceCategoty
            var Sc1 = new ServiceCategory { Name = "Лабораторная диагностика" , Description="Обширный спектр лабораторных услуг...",Doctors=doclist1};
            var Sc2 = new ServiceCategory { Name = "Узи",Description= "УЗИ является одним из самых результативных методов диагностики в современной медицине на сегодняшний...", Doctors = doclist2 };

            db.ServiceCategories.AddRange(Sc1, Sc2);

            db.SaveChanges();
        }
    }
}
