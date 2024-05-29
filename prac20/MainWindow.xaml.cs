using Microsoft.EntityFrameworkCore;
using prac20.models;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prac20
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        bool Switch = false;
        void LoadDBInDataGrid()
        {
            using (Praktos20Context _db = new Praktos20Context())
            {
                int SelectedIndex = dg1.SelectedIndex;
                _db.Заказыs.Load();
                _db.СведенияОклиентахs.Load();
                _db.СправочникВидовРаботs.Load();
                _db.СправочникИсполнителейРаботs.Load();
                dg1.ItemsSource = _db.Заказыs.ToList();
                if (SelectedIndex != -1)
                {
                    if (SelectedIndex == dg1.Items.Count) SelectedIndex--;
                    dg1.SelectedIndex = SelectedIndex;
                    dg1.ScrollIntoView(dg1.SelectedItem);
                }
                dg1.Focus();
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Data.Заказы = null;
            AE f = new AE();
            f.Owner = this;
            f.ShowDialog();
            LoadDBInDataGrid();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dg1.SelectedItem != null)
            {
                Data.Заказы = (Заказы)dg1.SelectedItem;
                AE f = new AE();
                f.Owner = this;
                f.ShowDialog();
                LoadDBInDataGrid();
            }   
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Уэээээ");
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Заказы row = (Заказы)dg1.SelectedItem;
                    if (row != null)
                    {
                        using (Praktos20Context _db = new Praktos20Context())
                        {
                            _db.Заказыs.Remove(row);
                            _db.SaveChanges();
                        }
                        LoadDBInDataGrid();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка удаления");
                }
            }
            else dg1.Focus();
        }

        private void Window_Loaded1(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void btnResult_Click(object sender, RoutedEventArgs e)
        {
            using (Praktos20Context _db = new Praktos20Context())
            {
                if (rd1.IsChecked == true)
                {
                    var queryForTask2 = from Заказы in _db.Заказыs
                                        where Заказы.КодИсполнителяNavigation.Адрес.StartsWith("у") || Заказы.КодИсполнителяNavigation.Адрес.StartsWith("п")
                                        select new
                                        {
                                            Наименование_работы = Заказы.КодРаботыNavigation.НаименованиеРаботы,
                                            Код_исполнителя = Заказы.КодИсполнителя,
                                            Адрес_исполнителя = Заказы.КодИсполнителяNavigation.Адрес
                                        }; ;

                    dg2.ItemsSource = queryForTask2.ToList();

                    MessageBox.Show("Были выбраны исполнители чьи адрема начинаются на М С Т");
                }
                else if (rd2.IsChecked == true)
                {
                    var queryForTask3 = from order in _db.Заказыs
                                        group order by order.НомерЗаказа into grouped
                                        select new
                                        {
                                            Номер_заказа = grouped.Key,
                                            Стоимость = grouped.Sum(o => o.КодРаботыNavigation.СтоимостьРаботы ?? 0)

                                        };

                    dg2.ItemsSource = queryForTask3.ToList();
                    MessageBox.Show("Была подсчитана стоимость заказа");
                }
                else if (rd3.IsChecked == true)
                {
                    СправочникИсполнителейРабот исполнитель = new СправочникИсполнителейРабот
                    {
                        НаименованиеОрганизации = tbOrgName.Text,
                        Адрес = tbAdres.Text,
                        Телефон = tbSmartphoneVivo.Text
                    };

                    _db.СправочникИсполнителейРаботs.Add(исполнитель);
                    _db.SaveChanges();
                }
                else if (rd4.IsChecked == true)
                {
                    var queryForTask2 = _db.СправочникВидовРаботs
                     .Join(_db.СправочникИсполнителейРаботs,
                             work => work.КодРаботы,
                          performer => performer.КодИсполнителя,
                         (work, performer) => new
                         {
                             Наименование_организации = performer.НаименованиеОрганизации,
                             Наименование_работы = work.НаименованиеРаботы,
                             Премия = work.СтоимостьРаботы * 0.1m
                         });
                    dg2.ItemsSource = queryForTask2.ToList();
                }
                else if (rd5.IsChecked == true)
                {
                    var queryForTask3 = _db.СведенияОклиентахs
                        .Select(o => new
                        {
                            Наименование_объекта = o.НаименованиеОбъекта,
                            Количество_автомобилей = o.Заказыs.Select(z => z.МаркаАвтомобиля).Distinct().Count()
                        });
                    dg2.ItemsSource = queryForTask3.ToList();
                }
                else if (rd6.IsChecked == true)
                {
                    var queryForTask4 = _db.Заказыs
                        .Where(z => z.Дата.HasValue && z.Дата.Value.Month >= 6 && z.Дата.Value.Month <= 8)
                        .GroupBy(z => z.МаркаАвтомобиля)
                        .Select(g => new
                        {
                             Марка_автомобиля = g.Key,
                             Количество_заказов = g.Count()
                        });
                    dg2.ItemsSource = queryForTask4.ToList();
                }
                else if (rd7.IsChecked == true)
                {
                    var queryForTask5 = _db.Заказыs
                        .OrderByDescending(z => z.КодРаботыNavigation.СтоимостьРаботы)
                        .Select(z => new
                        {
                                Клиент = z.Клиент,
                                Номер_заказа = z.НомерЗаказа,
                                Стоимость = z.КодРаботыNavigation.СтоимостьРаботы
                        }).FirstOrDefault();

                    dg2.ItemsSource = new List<object> { queryForTask5 };
                }
                else
                {
                    MessageBox.Show("Ошибка, выберите запрос");
                }
            }
                
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            List<Заказы> listitem = (List<Заказы>)dg1.ItemsSource;
            var filtered = listitem.Where(p => p.Клиент.Contains(tbName.Text));
            if (filtered.Count() > 0)
            {
                var item = filtered.First();
                dg1.SelectedItem = item;
                dg1.ScrollIntoView(item);
                dg1.Focus();
            }
        }
        private void cbClick(object sender, RoutedEventArgs e)
        {
            if (Switch == false)
            {
                gbox1.IsEnabled = false;
                rd1.IsChecked = false;
                rd2.IsChecked = false;
                gbox2.IsEnabled = true;
                gbPashalko.Visibility = Visibility.Visible;
                Switch = true;
            }
            else if (Switch == true)
            {
                gbox1.IsEnabled = true;
                rd3.IsChecked = false;
                rd4.IsChecked = false;
                rd5.IsChecked = false;
                rd6.IsChecked = false;
                rd7.IsChecked = false;
                gbox2.IsEnabled = false;
                gbPashalko.Visibility = Visibility.Hidden;
                Switch = false  ;
            }
        }

        
    }
}