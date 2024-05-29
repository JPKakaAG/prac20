using prac20.models;
using System;
using System.Collections.Generic;
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

namespace prac20
{
    /// <summary>
    /// Логика взаимодействия для AE.xaml
    /// </summary>
    public partial class AE : Window
    {
        public AE()
        {
            InitializeComponent();
        }
       
        Praktos20Context _db = new Praktos20Context();
        Заказы _заказы;
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (tbNZ.Text == null)
            {
                errors.Append("Напишите номер заказа");
            }
            if (tbMark == null)
            {
                errors.Append("Напишите марку авто");
            }
            if (cbClient.SelectedItem == null)
            {
                errors.Append("Выберите клиента");
            }
            if (cbWork.SelectedItem == null)
            {
                errors.Append("Выберите услугу");
            }

            if (cbWorker.SelectedItem == null)
            {
                errors.Append("Выберите исполнителя");
            }
            try
            {
                if (Data.Заказы == null)
                {
                    _db.Заказыs.Add(_заказы);
                    _db.SaveChanges();
                }
                else
                {
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbClient.ItemsSource = _db.СведенияОклиентахs.ToList();
            cbClient.DisplayMemberPath = "Клиент";
            cbWork.ItemsSource = _db.СправочникВидовРаботs.ToList();
            cbWork.DisplayMemberPath = "НаименованиеРаботы";
            cbWorker.ItemsSource = _db.СправочникИсполнителейРаботs.ToList();
            cbWorker.DisplayMemberPath = "НаименованиеОрганизации";
            if (Data.Заказы == null)
            {
                AE1.Title = "Добавление записи";
                btnAddEdit.Content = "Добавить";
                _заказы = new Заказы();
            }
            else
            {
                AE1.Title = "Изменение записи";
                btnAddEdit.Content = "Изменить";
                _заказы = _db.Заказыs.
                    Find(Data.Заказы.НомерЗаказа);
            }
            AE1.DataContext = _заказы;
        }
    }
}
