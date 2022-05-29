using ecl.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ecl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DbContextOptions<ApplicationContext> options;

        // Коллекции автомобилей, клиентов и заказов
        ObservableCollection<Company> companies = new ObservableCollection<Company>();
        ObservableCollection<Person> persons = new ObservableCollection<Person>();
        ObservableCollection<OrgRegistration> orgRegistrations = new ObservableCollection<OrgRegistration>();
        ObservableCollection<OrgLegForm> orgLegForms = new ObservableCollection<OrgLegForm>();


        public MainWindow()
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

        }


        /// <summary>
        /// Загрузка автомобилей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btLoad_Click(object sender, RoutedEventArgs e)
        {
            ProgressBarAgreement.Visibility = Visibility.Visible;

            //ObservableCollection<Car> listCars = new ObservableCollection<Car>();

            Task tc = Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext(options))
                {
                    var query = db.Companies
                    .Include(d => d.Person)
                    .Include(d => d.OrgRegistration)
                    .Include(d => d.OrgLegForm);


                    if (query.Count() != 0)
                    {
                        foreach (var c in query)
                            companies.Add(c);
                    }
                }
            });

            await tc;

            await Task.Run(() => Thread.Sleep(200));

            ProgressBarAgreement.Visibility = Visibility.Collapsed;
            lvCompany.ItemsSource = companies;
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            Company newCompany = new Company();
            WindowCompany editWindowCompany = new WindowCompany();
            editWindowCompany.Title = "Добавление новой ориганизации";
            editWindowCompany.DataContext = newCompany;
            editWindowCompany.ShowDialog();

            if (editWindowCompany.DialogResult == true)
            {
                using (ApplicationContext db = new ApplicationContext(options))
                {
                    try
                    {
                        db.Companies.Add(newCompany);
                        db.SaveChanges();
                        companies.Add(newCompany);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("\nОшибка добавления данных!\n" + ex.Message, "Предупреждение");
                    }
                }
            }

        }

        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            Company edit = (Company)lvCompany.SelectedItem;

            WindowCompany editWindowCompany = new WindowCompany();
            editWindowCompany.Title = "Редактирование данных по клиенту";
            editWindowCompany.DataContext = edit;
            editWindowCompany.ShowDialog();



            if (editWindowCompany.DialogResult == true)
            {
                using (ApplicationContext db = new ApplicationContext(options))
                {
                    Company com = db.Companies.Find(edit.Id);

                    if (com.NameFull != edit.NameFull)
                        com.NameFull = edit.NameFull;
                    if (com.NameShort != edit.NameShort)
                        com.NameShort = edit.NameShort;
                    if (com.NumberReg != edit.NumberReg)
                        com.NumberReg = edit.NumberReg;
                    if (com.DataReg != edit.DataReg)
                        com.DataReg = edit.DataReg;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("\nОшибка редактирования данных!\n" + ex.Message, "Предупреждение");
                    }
                }
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            Company delCompany = (Company)lvCompany.SelectedItem;

            using (ApplicationContext db = new ApplicationContext(options))
            {
                // Поиск в контексте удаляемого автомобиля
                Company del = db.Companies.Find(delCompany.Id);

                if (del != null)
                {
                    MessageBoxResult result = MessageBox.Show("Удалить данные по клиенту: \n" + del.NameFull + "  " + del.NameShort + " " + del.NumberReg + " " + del.DataReg,
                  "Предупреждение", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        try
                        {
                            db.Companies.Remove(del);
                            db.SaveChanges();
                            companies.Remove(delCompany);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("\nОшибка удаления данных!\n" + ex.Message, "Предупреждение");
                        }
                    }
                }
            }
        }

        private async void btLoadPer_Click(object sender, RoutedEventArgs e)
        {
            //using (ApplicationContext db = new ApplicationContext(options))
            //{
            //    var custs = db.Customers.ToList();
            //    lvCustomer.ItemsSource = custs;
            //}

            ProgressBarPerson.Visibility = Visibility.Visible;

            persons.Clear();

            Task tc = Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext(options))
                {
                    var query = db.Persons;
                    if (query.Count() != 0)
                    {
                        foreach (var c in query)
                            persons.Add(c);
                    }
                }
            });

            await tc;

            await Task.Run(() => Thread.Sleep(200));

            ProgressBarPerson.Visibility = Visibility.Collapsed;
            lvPerson.ItemsSource = persons;
        }

        private void btAddPer_Click(object sender, RoutedEventArgs e)
        {
            Person newPerson = new Person();
            WindowPerson editWindowPerson = new WindowPerson();
            editWindowPerson.Title = "Добавление нового клиента";
            editWindowPerson.DataContext = newPerson;
            editWindowPerson.ShowDialog();

            if (editWindowPerson.DialogResult == true)
            {
                using (ApplicationContext db = new ApplicationContext(options))
                {
                    try
                    {
                        db.Persons.Add(newPerson);
                        db.SaveChanges();
                        persons.Add(newPerson);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("\nОшибка добавления данных!\n" + ex.Message, "Предупреждение");
                    }
                }
            }

        }

        private void btEditPer_Click(object sender, RoutedEventArgs e)
        {
            Person editPer = (Person)lvPerson.SelectedItem;

            WindowPerson editWindowPerson = new WindowPerson();
            editWindowPerson.Title = "Редактирование данных по клиенту";
            editWindowPerson.DataContext = editPer;
            editWindowPerson.ShowDialog();



            if (editWindowPerson.DialogResult == true)
            {
                using (ApplicationContext db = new ApplicationContext(options))
                {
                    Person per = db.Persons.Find(editPer.Id);

                    if (per.Shifer != editPer.Shifer)
                        per.Shifer = editPer.Shifer;
                    if (per.Inn != editPer.Inn)
                        per.Inn = editPer.Inn;
                    if (per.Type != editPer.Type)
                        per.Type = editPer.Type;
                    if (per.Data != editPer.Data)
                        per.Data = editPer.Data;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("\nОшибка редактирования данных!\n" + ex.Message, "Предупреждение");
                    }
                }
            }

        }

        private void btDeletePer_Click(object sender, RoutedEventArgs e)
        {
            Person delPerson = (Person)lvPerson.SelectedItem;

            using (ApplicationContext db = new ApplicationContext(options))
            {
                // Поиск в контексте удаляемого автомобиля
                Person delPer = db.Persons.Find(delPerson.Id);

                if (delPer != null)
                {
                    MessageBoxResult result = MessageBox.Show("Удалить данные по клиенту: \n" + delPer.Shifer + " " + delPer.Inn + "  " + delPer.Type + " " + delPer.Data,
                  "Предупреждение", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        try
                        {
                            db.Persons.Remove(delPer);
                            db.SaveChanges();
                            persons.Remove(delPerson);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("\nОшибка удаления данных!\n" + ex.Message, "Предупреждение");
                        }
                    }
                }
            }
        }

        private async void btLoadOR_Click(object sender, RoutedEventArgs e)
        {
            //using (ApplicationContext db = new ApplicationContext(options))
            //{
            //    var ords = db.Orders
            //        .Include(c => c.Car)
            //        .Include(p => p.Cust)
            //        .ToList();

            //    lvOrder.ItemsSource = ords;
            //}

            ProgressBarOrgRegistration.Visibility = Visibility.Visible;

            Task tc = Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext(options))
                {

                    var query = db.OrgRegistrations;
                    if (query.Count() != 0)
                    {
                        foreach (var c in query)
                            orgRegistrations.Add(c);
                    }
                }
            });

            await tc;

            await Task.Run(() => Thread.Sleep(200));

            ProgressBarOrgRegistration.Visibility = Visibility.Collapsed;
            lvOrgRegistration.ItemsSource = orgRegistrations;
        }

        private void btAddOR_Click(object sender, RoutedEventArgs e)
        {
            OrgRegistration newOrgRegistration = new OrgRegistration();
            WindowOrgRegistration editWindowOrgRegistration = new WindowOrgRegistration();
            editWindowOrgRegistration.Title = "Добавление нового клиента";
            editWindowOrgRegistration.DataContext = newOrgRegistration;
            editWindowOrgRegistration.ShowDialog();

            if (editWindowOrgRegistration.DialogResult == true)
            {
                using (ApplicationContext db = new ApplicationContext(options))
                {
                    try
                    {
                        db.OrgRegistrations.Add(newOrgRegistration);
                        db.SaveChanges();
                        orgRegistrations.Add(newOrgRegistration);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("\nОшибка добавления данных!\n" + ex.Message, "Предупреждение");
                    }
                }
            }
        }

        private void btEditOR_Click(object sender, RoutedEventArgs e)
        {
            OrgRegistration editOR = (OrgRegistration)lvOrgRegistration.SelectedItem;

            WindowOrgRegistration editWindowOrgRegistration = new WindowOrgRegistration();
            editWindowOrgRegistration.Title = "Редактирование данных по клиенту";
            editWindowOrgRegistration.DataContext = editOR;
            editWindowOrgRegistration.ShowDialog();



            if (editWindowOrgRegistration.DialogResult == true)
            {
                using (ApplicationContext db = new ApplicationContext(options))
                {
                    OrgRegistration or = db.OrgRegistrations.Find(editOR.Id);

                    if (or.NameFull != editOR.NameFull)
                        or.NameFull = editOR.NameFull;
                    if (or.NameShort != editOR.NameShort)
                        or.NameShort = editOR.NameShort;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("\nОшибка редактирования данных!\n" + ex.Message, "Предупреждение");
                    }
                }
            }
        }

        private void btDeleteOR_Click(object sender, RoutedEventArgs e)
        {
            OrgRegistration delOrgRegistration = (OrgRegistration)lvOrgRegistration.SelectedItem;

            using (ApplicationContext db = new ApplicationContext(options))
            {
                // Поиск в контексте удаляемого автомобиля
                OrgRegistration delOR = db.OrgRegistrations.Find(delOrgRegistration.Id);

                if (delOR != null)
                {
                    MessageBoxResult result = MessageBox.Show("Удалить данные по клиенту: \n" + delOR.NameFull + " " + delOR.NameShort,
                  "Предупреждение", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        try
                        {
                            db.OrgRegistrations.Remove(delOR);
                            db.SaveChanges();
                            orgRegistrations.Remove(delOrgRegistration);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("\nОшибка удаления данных!\n" + ex.Message, "Предупреждение");
                        }
                    }
                }
            }
        }

        private async void btLoadOLF_Click(object sender, RoutedEventArgs e)
        {
            //using (ApplicationContext db = new ApplicationContext(options))
            //{
            //    var ords = db.Orders
            //        .Include(c => c.Car)
            //        .Include(p => p.Cust)
            //        .ToList();

            //    lvOrder.ItemsSource = ords;
            //}

            ProgressBarOrgLegForm.Visibility = Visibility.Visible;

            Task tc = Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext(options))
                {

                    var query = db.OrgLegForms;
                    if (query.Count() != 0)
                    {
                        foreach (var c in query)
                            orgLegForms.Add(c);
                    }
                }
            });

            await tc;

            await Task.Run(() => Thread.Sleep(200));

            ProgressBarOrgLegForm.Visibility = Visibility.Collapsed;
            lvOrgLegForm.ItemsSource = orgLegForms;
        }

        private void btAddOLF_Click(object sender, RoutedEventArgs e)
        {
            OrgLegForm newOrgLegForm = new OrgLegForm();
            WindowOrgLegForm editWindowOrgLegForm = new WindowOrgLegForm();
            editWindowOrgLegForm.Title = "Добавление нового клиента";
            editWindowOrgLegForm.DataContext = newOrgLegForm;
            editWindowOrgLegForm.ShowDialog();

            if (editWindowOrgLegForm.DialogResult == true)
            {
                using (ApplicationContext db = new ApplicationContext(options))
                {
                    try
                    {
                        db.OrgLegForms.Add(newOrgLegForm);
                        db.SaveChanges();
                        orgLegForms.Add(newOrgLegForm);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("\nОшибка добавления данных!\n" + ex.Message, "Предупреждение");
                    }
                }
            }
        }

        private void btEditOLF_Click(object sender, RoutedEventArgs e)
        {
            OrgLegForm editOLF = (OrgLegForm)lvOrgLegForm.SelectedItem;

            WindowOrgLegForm editWindowOrgLegForm = new WindowOrgLegForm();
            editWindowOrgLegForm.Title = "Редактирование данных по клиенту";
            editWindowOrgLegForm.DataContext = editOLF;
            editWindowOrgLegForm.ShowDialog();



            if (editWindowOrgLegForm.DialogResult == true)
            {
                using (ApplicationContext db = new ApplicationContext(options))
                {
                    OrgLegForm olf = db.OrgLegForms.Find(editOLF.Id);

                    if (olf.NameFull != editOLF.NameFull)
                        olf.NameFull = editOLF.NameFull;
                    if (olf.NameShort != editOLF.NameShort)
                        olf.NameShort = editOLF.NameShort;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("\nОшибка редактирования данных!\n" + ex.Message, "Предупреждение");
                    }
                }
            }
        }

        private void btDeleteOLF_Click(object sender, RoutedEventArgs e)
        {
            OrgLegForm delOrgLegForm = (OrgLegForm)lvOrgLegForm.SelectedItem;

            using (ApplicationContext db = new ApplicationContext(options))
            {
                // Поиск в контексте удаляемого автомобиля
                OrgLegForm delOLF = db.OrgLegForms.Find(delOrgLegForm.Id);

                if (delOLF != null)
                {
                    MessageBoxResult result = MessageBox.Show("Удалить данные по клиенту: \n" + delOLF.NameFull + " " + delOLF.NameShort,
                  "Предупреждение", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        try
                        {
                            db.OrgLegForms.Remove(delOLF);
                            db.SaveChanges();
                            orgLegForms.Remove(delOrgLegForm);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("\nОшибка удаления данных!\n" + ex.Message, "Предупреждение");
                        }
                    }
                }
            }
        }
    }
}
