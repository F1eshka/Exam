using EkzamenADO.DataAccess;
using EkzamenADO.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace EkzamenADO
{
    public partial class AddAdWindow : Window
    {
        private readonly DbManager db;
        private readonly User currentUser;
        private string? selectedImageFileName;

        public AddAdWindow(User user)
        {
            InitializeComponent();

            db = new DbManager();
            currentUser = user;

            // Загружаем список категорий из базы данных
            CategoryBox.ItemsSource = db.GetAllCategories();
            CategoryBox.DisplayMemberPath = "Name";
            CategoryBox.SelectedValuePath = "Id";
        }

        // Обработчик кнопки выбора изображения
        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Оберіть зображення",
                Filter = "Файли зображень (*.png;*.jpg)|*.png;*.jpg"
            };

            if (dialog.ShowDialog() == true)
            {
                string fileName = Path.GetFileName(dialog.FileName);
                string appImageDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
                string destinationPath = Path.Combine(appImageDir, fileName);

                // Создаем папку, если её нет
                if (!Directory.Exists(appImageDir))
                {
                    Directory.CreateDirectory(appImageDir);
                }

                // Копируем изображение, если ещё не скопировано
                if (!File.Exists(destinationPath))
                {
                    File.Copy(dialog.FileName, destinationPath);
                }

                selectedImageFileName = fileName;
                ImageLabel.Text = fileName;
            }
        }

        // Обработчик кнопки "Додати"
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text) ||
                string.IsNullOrWhiteSpace(DescriptionBox.Text) ||
                string.IsNullOrWhiteSpace(PriceBox.Text) ||
                CategoryBox.SelectedItem == null)
            {
                MessageBox.Show("Будь ласка, заповніть усі поля.");
                return;
            }

            if (!decimal.TryParse(PriceBox.Text, out decimal price))
            {
                MessageBox.Show("Невірний формат ціни.");
                return;
            }

            Ad newAd = new Ad
            {
                Title = TitleBox.Text,
                Description = DescriptionBox.Text,
                Price = price,
                CategoryId = (int)CategoryBox.SelectedValue!,
                ImageFileName = selectedImageFileName ?? "",
                UserId = currentUser.Id,
                CreatedAt = DateTime.Now
            };

            try
            {
                db.AddAd(newAd);
                MessageBox.Show("Оголошення додано успішно!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при додаванні оголошення: {ex.Message}");
            }
        }
    }
}
