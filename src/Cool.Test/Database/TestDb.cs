using System;
using System.Collections.Generic;
using System.Text;
using Cool.Dal;
using Cool.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cool.Test.Database
{
    public static class TestDb
    {
        public static CoolDbContext GetContext()
        {
            var context = new CoolDbContext(new DbContextOptionsBuilder<CoolDbContext>()
                .UseSqlite("Filename=Test.db")
                .Options); 

            Seed(context);
            return context;
        }

        public static void Seed(CoolDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            dbContext.Caffs.Add(new Caff
            {
                Id = 1,
                CreationTime = new DateTime(2021, 11, 29, 19, 00, 00),
                Creator = "testUser1",
                FilePath = "../CaffFiles/1.caff",
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Id = 1, Message = "A comment", UserName = "testUser1",
                        TimeStamp = new DateTime(2021, 11, 29, 19, 00, 00)
                    },
                    new Comment
                    {
                        Id = 2, Message = "Another comment", UserName = "testUser2",
                        TimeStamp = new DateTime(2021, 11, 29, 19, 30, 00)
                    },
                },
                Tags = new List<Tag>
                {
                    new Tag
                    {
                        Id = 1, Text = "A tag"
                    },
                    new Tag
                    {
                        Id = 2, Text = "Another tag"
                    }
                }
            });

            dbContext.Caffs.Add(new Caff
            {
                Id = 2,
                CreationTime = new DateTime(2021, 11, 30, 13, 00, 00),
                Creator = "testUser2",
                FilePath = "../CaffFiles/2.caff",
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Id = 3, Message = "A comment", UserName = "testUser2",
                        TimeStamp = new DateTime(2021, 11, 29, 19, 00, 00)
                    },
                    new Comment
                    {
                        Id = 4, Message = "Another comment", UserName = "testUser2",
                        TimeStamp = new DateTime(2021, 11, 29, 19, 30, 00)
                    },
                },
                Tags = new List<Tag>
                {
                    new Tag
                    {
                        Id = 3, Text = "A tag"
                    },
                    new Tag
                    {
                        Id = 4, Text = "Special"
                    }
                }
            });

            dbContext.SaveChanges();
        }
    }
}
