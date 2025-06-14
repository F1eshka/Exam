using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using EkzamenADO.DataAccess;
using EkzamenADO.Models;

namespace EkzamenADO
{
    public partial class EditAdWindow : Window
    {
        private readonly DbManager db;
        private readonly User currentUser;
        private readonly int adId;
        private Ad currentAd;
        private string selectedImageFileName;

        public EditAdWindow(User user, int adId)
        {
            InitializeComponent();
            db = new DbManager();
            currentUser = user;
            this.adId = adId;

            CategoryBox.ItemsSource = db.GetAllCategories();
            CategoryBox.DisplayMemberPath = "Name";
            CategoryBox.SelectedValuePath = "Id";

            LoadAd();
        }

        private void LoadAd()
        {
            currentAd = db.GetAdById(adId);
            if (currentAd == null)
            {
                MessageBox.Show("Оголошення не знайдено");
                Close();
                return;
            }

            TitleBox.Text = currentAd.Title;
            DescriptionBox.Text = currentAd.Description;
            PriceBox.Text = currentAd.Price.ToString();

            foreach (Category cat in CategoryBox.Items)
            {
                if (cat.Id == currentAd.CategoryId)
                {
                    CategoryBox.SelectedItem = cat;
                    break;
                }
            }

            selectedImageFileName = currentAd.ImageFileName;
            ImageFileNameTextBlock.Text = $"Поточне зображення: {selectedImageFileName}";
        }

        private void ChooseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (dialog.ShowDialog() == true)
            {
                string selectedFile = dialog.FileName;
                string imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");

                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                string fileName = Path.GetFileName(selectedFile);
                string destinationPath = Path.Combine(imagesFolder, fileName);

                try
                {
                    File.Copy(selectedFile, destinationPath, true);
                    selectedImageFileName = fileName;
                    ImageFileNameTextBlock.Text = $"Файл обрано: {fileName}";
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Помилка копіювання файлу: " + ex.Message);
                }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text) ||
                !decimal.TryParse(PriceBox.Text, out decimal price) ||
                CategoryBox.SelectedItem == null)
            {
                MessageBox.Show("Перевірте правильність заповнення полів");
                return;
            }

            currentAd.Title = TitleBox.Text;
            currentAd.Description = DescriptionBox.Text;
            currentAd.Price = price;
            currentAd.CategoryId = ((Category)CategoryBox.SelectedItem).Id;
            currentAd.ImageFileName = selectedImageFileName;

            db.UpdateAd(currentAd);
            MessageBox.Show("Оголошення оновлено");
            Close();
        }
    }
}
