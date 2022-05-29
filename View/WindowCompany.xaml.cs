using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ecl.View
{
    /// <summary>
    /// Логика взаимодействия для WindowCompany.xaml
    /// </summary>
    public partial class WindowCompany : Window
    {
        public List<Person> ListPerson;
        public List<OrgRegistration> Listorgregistration;
        public List<OrgLegForm> Listorglegform;

        DbContextOptions<ApplicationContext> options;
        public WindowCompany()
        {
            InitializeComponent();
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            options = optionsBuilder.UseSqlServer(connectionString).Options;

            using (ApplicationContext db = new ApplicationContext(options))
            {
                ListPerson = db.Persons.ToList<Person>();
                Listorgregistration = db.OrgRegistrations.ToList<OrgRegistration>();
                Listorglegform = db.OrgLegForms.ToList<OrgLegForm>();

            }
        }
        private void btOK_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
