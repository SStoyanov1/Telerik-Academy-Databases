namespace _11.UserGroupsTransaction
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Transactions;

    public class UserGroupsTransaction
    {
        static void Main()
        {
            UsersGroupsEntities1 userContex = new UsersGroupsEntities1();

            using (userContex)
            {
                using (var dbContextTransaction = userContex.Database.BeginTransaction())
                {
                    try
                    {
                        Group adminGroup = new Group { GroupId = 2, GroupName = "Admins" };

                        if (userContex.Groups.Where(g => g.GroupName == "Admins").ToList().Count == 0)
                        {
                            userContex.Groups.Add(adminGroup);
                            userContex.SaveChanges();
                        }

                        User user = new User { UserId = 1, UserName = "Pesho", Password = "1234", GroupId = 2 };

                        userContex.Users.Add(user);
                        userContex.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        dbContextTransaction.Rollback();
                    } 
                }
            }
        }
    }
}
