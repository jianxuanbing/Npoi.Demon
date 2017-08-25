using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Npoi.DoWord.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<string> sqls=new List<string>();
            sqls = WordHelper.GenerateCreateTableSql(@"D:\紫云来数据库设计.docx", "BasScenicTicketType", "BasScenicTicket",
                "BusScenicOrder", "BusScenicOrderTicket", "BasHotelRoomType", "BasHotelRoom", "BusHotelOrder",
                "BusHotelOrderRoom", "BasCourse", "BasCourseSchedule", "BasCourseOrder");
            foreach (string sql in sqls)
            {
                Console.WriteLine(sql);
            }
        }
    }
}
