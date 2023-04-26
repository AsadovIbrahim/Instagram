using AdminNamespace.PostNamespace;
using AdminNamespace.UserNamespace;
using System.Net;
using System.Net.Mail;

namespace AdminNamespace
{
    public class Admin
    {
        public int ID;
        public string Username;
        public string Email;
        public string Password;
        public List<Post> Posts = new List<Post>();
        public List<Notification> Notifications = new List<Notification>();
    }
    public class Notification
    {
        public int ID;
        public string Text;
        public DateTime DateTime;
        public User FromUser;
    }
    namespace UserNamespace
    {
        public class User
        {
            public int ID;
            public string Name;
            public string Surname;
            public int Age;
            public string Email;
            public string Password;
        }
    }
    namespace PostNamespace
    {
        public class Post
        {
            public int ID;
            public string Content;
            public DateTime CreationDateTime;
            public int LikeCount;
            public int ViewCount;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            bool isAdmin = false;
            AdminNamespace.Admin currentAdmin = null;
            UserNamespace.User currentUser = null;

            List<AdminNamespace.Admin> admins = new List<AdminNamespace.Admin>();
            admins.Add(new AdminNamespace.Admin
            {
                ID = 1,
                Username = "admin",
                Email = "admin@example.com",
                Password = "admin",
                Notifications = new List<Notification>()
            });

            List<UserNamespace.User> users = new List<UserNamespace.User>();
            users.Add(new UserNamespace.User
            {
                ID = 1,
                Name = "Ibrahim",
                Surname = "Asadov",
                Age = 18,
                Email = "ibrahimasadov31@gmail.com",
                Password = "ibrahim123"
            });

            users.Add(new UserNamespace.User
            {
                ID = 2,
                Name = "Ibrahim",
                Surname = "Asadov",
                Age = 18,
                Email = "ibrahimasadov31@gmail.com",
                Password = "ibrahim123"
            });

            List<Post> posts = new List<Post>();
            posts.Add(new Post { ID = 1, Content = "Post Content:1", CreationDateTime = DateTime.Now, LikeCount = 0, ViewCount = 0 });
            posts.Add(new Post { ID = 2, Content = "Post Content:2", CreationDateTime = DateTime.Now, LikeCount = 0, ViewCount = 0 });
            posts.Add(new Post { ID = 3, Content = "Post Content:3", CreationDateTime = DateTime.Now, LikeCount = 0, ViewCount = 0 });
            
               

                bool status = true;
                static void SetConsoleColor(string option)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine(option);
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                dynamic key;
                int choose = 0;
                Console.ResetColor();

            while (status)
            {
                key = Console.ReadKey();

                switch (key.Key)
                {
                    case 27:
                        status = false;
                        break;
                    case ConsoleKey.UpArrow:
                        if (choose != 0) choose--;
                        else choose = 1;
                        Console.Clear();
                        break;
                    case ConsoleKey.DownArrow:
                        if (choose != 1) choose++;
                        else choose = 0;
                        Console.Clear();

                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        if (choose == 0)
                        {
                            
                                Console.WriteLine("Enter Your Username or (Email):");
                                string username = Console.ReadLine();

                                Console.WriteLine("Enter Password:");
                                string password = Console.ReadLine();

                            if (username == "admin" && password == "admin")
                            {
                                isAdmin = true;
                                currentAdmin = admins[0];
                                Console.WriteLine("Welcome Admin!");
                                Console.WriteLine("All Posts:");
                                foreach (Post post in posts)
                                {
                                    Console.WriteLine("{0}. {1} - Likes: {2} - Views: {3}", post.ID, post.Content, post.LikeCount, post.ViewCount);
                                }
                                int postId = int.Parse(Console.ReadLine());
                                Post postLike = null;
                                foreach (Post post in posts)
                                {
                                    if (post.ID == postId)
                                    {
                                        postLike = post;
                                    }


                                }
                                postLike.ViewCount++;
                                Console.WriteLine("Post {0} viewed!", postLike.ViewCount);
                                Notification notification = new Notification
                                {
                                    ID = currentAdmin.Notifications.Count + 1,
                                    Text = "Post " + postLike.ID + " liked by " + (isAdmin ? "admin" : currentUser.Name),
                                    DateTime = DateTime.Now,
                                    FromUser = currentUser
                                };
                                currentAdmin.Notifications.Add(notification);
                                Console.WriteLine("Notification Created!");

                                var fromAddress = new MailAddress("ibrahimasadov31@gmail.com", "Instagram");
                                var toAddress = new MailAddress("rustamh2006@gmail.com");
                                string fromPassword = "wzktlxuijxjewkae";
                                const string subject = "Instagram";
                                string body = "Post " + postLike.ID + " has viewed";

                                var smtp = new SmtpClient
                                {
                                    Host = "smtp.gmail.com",
                                    Port = 587,
                                    EnableSsl = true,
                                    DeliveryMethod = SmtpDeliveryMethod.Network,
                                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                                    Timeout = 20000
                                };

                                using (var message = new MailMessage(fromAddress, toAddress)
                                {
                                    Subject = subject,
                                    Body = body
                                })
                                {
                                    smtp.Send(message);
                                }
                                Console.WriteLine("Email sent!");

                            }
                            else
                            {
                                try
                                {

                                    throw new Exception("Invalid Username Or Password!");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);

                                }
                                Thread.Sleep(2000);
                            }

                        }
                        else if (choose == 1)
                        {
                            Console.WriteLine("Enter Your Username or (Email):");
                            string username = Console.ReadLine();

                            Console.WriteLine("Enter Password:");
                            string password = Console.ReadLine();
                            if (username == "ibrahim" && password == "ibrahim123")
                            {
                                isAdmin = true;
                                currentAdmin = admins[0];
                                Console.WriteLine("Welcome Admin!");
                                Console.Clear();
                                Console.WriteLine("All Posts:");
                                foreach (Post post in posts)
                                {
                                    Console.WriteLine("{0}. {1} - Likes: {2} - Views: {3}", post.ID, post.Content, post.LikeCount, post.ViewCount);
                                }
                                Console.WriteLine("Which Post Do You Want To Like:");
                                Console.WriteLine("Enter The ID Of The Post You Would Like To Like:");
                                int postId = int.Parse(Console.ReadLine());
                                Post postLike = null;
                                foreach (Post post in posts)
                                {
                                    if (post.ID == postId)
                                    {
                                        postLike = post;
                                        break;
                                    }
                                }
                                if (postLike == null)
                                {
                                    Console.WriteLine("Post Didn't Found!");
                                    Console.Clear();
                                }
                                else
                                {
                                    postLike.LikeCount++;
                                    postLike.ViewCount++;
                                    Console.WriteLine("Post {0} liked!", postLike.ID);
                                    Notification notification = new Notification
                                    {
                                        ID = currentAdmin.Notifications.Count + 1,
                                        Text = "Post " + postLike.ID + " liked by " + (isAdmin ? "admin" : currentUser.Name),
                                        DateTime = DateTime.Now,
                                        FromUser = currentUser
                                    };
                                    currentAdmin.Notifications.Add(notification);
                                    Console.WriteLine("Notification Created!");
                                    Thread.Sleep(1000);

                                    var fromAddress = new MailAddress("ibrahimasadov31@gmail.com", "Instagram");
                                    var toAddress = new MailAddress("rustamh2006@gmail.com");
                                    string fromPassword = "wzktlxuijxjewkae";
                                    const string subject = "Instagram";
                                    string body = "Post " + postLike.ID + " has liked";

                                    var smtp = new SmtpClient
                                    {
                                        Host = "smtp.gmail.com",
                                        Port = 587,
                                        EnableSsl = true,
                                        DeliveryMethod = SmtpDeliveryMethod.Network,
                                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                                        Timeout = 20000
                                    };

                                    using (var message = new MailMessage(fromAddress, toAddress)
                                    {
                                        Subject = subject,
                                        Body = body
                                    })
                                    {
                                        smtp.Send(message);
                                    }




                                    Console.WriteLine("Email sent!");
                                }

                            }

                            else
                            {
                                try
                                {

                                    throw new Exception("Invalid Username Or Password!");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);

                                }
                                Thread.Sleep(2000);


                            }
                        }

                        break;

                }
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow ;

                Console.WriteLine(
           "\t\t\t                                  \n" +
           "\t\t\t         @@@@@@@@@@@@@@@@@@@@@    \n" +
           "\t\t\t       @@@                   @@@  \n" +
           "\t\t\t      @@                 @@    @@ \n" +
           "\t\t\t      @@        @@@@@@@        @@ \n" +
           "\t\t\t      @@      @@@     @@@      @@ \n" +
           "\t\t\t      @@     @@@       @@@     @@ \n" +
           "\t\t\t      @@      @@@     @@@      @@ \n" +
           "\t\t\t      @@        @@@@@@@        @@ \n" +
           "\t\t\t      @@                       @@ \n" +
           "\t\t\t       @@@                   @@@  \n" +
           "\t\t\t         @@@@@@@@@@@@@@@@@@@@@    \n");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(
                    "\t\t     _____          _                                  \n" +
                    "\t\t    |_   _|        | |                                 \n" +
                    "\t\t      | | _ __  ___| |_ __ _  __ _ _ __ __ _ _ __ ___  \n" +
                    "\t\t      | || '_ \\/ __| __/ _` |/ _` | '__/ _` | '_ ` _ \\ \n" +
                    "\t\t     _| || | | \\__ \\ || (_| | (_| | | | (_| | | | | | |\n" +
                    "\t\t     \\___/_| |_|___/\\__\\__,_|\\__, |_|  \\__,_|_| |_| |_|\n" +
                    "\t\t                              __/ |                    \n" +
                    "\t\t                             |___/                     \n");

                if (choose == 0) SetConsoleColor("\n\t\t\t\t\t->ADMIN<-");


                else Console.WriteLine("\n\t\t\t\t\tADMIN");


                if (choose == 1) SetConsoleColor("\t\t\t\t\t->USER<-");

                else Console.WriteLine("\t\t\t\t\tUSER");


            }

        }

    }
}
