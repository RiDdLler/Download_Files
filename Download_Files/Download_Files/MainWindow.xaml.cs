using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace FileDownloaderWPF
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private async void DownloadButton_Click(object sender, RoutedEventArgs e)
		{
			string url = urlTextBox.Text;

			if (Uri.TryCreate(url, UriKind.Absolute, out Uri uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
			{
				try
				{
					using (WebClient webClient = new WebClient())
					{
						//Путь для сохранения файла на компьютере
						string localFilePath = @"C:\Users\user\Downloads\Audi.jpg";//ИЗМЕНЯТЬ НАЗВАНИЕ

						// Загрузка файла асинхронно и сохранение его на компьютере
						await webClient.DownloadFileTaskAsync(uri, localFilePath);
						MessageBox.Show($"File downloaded successfully and saved to {localFilePath}");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Error: {ex.Message}");
				}
			}
			else
			{
				MessageBox.Show("Invalid URL. Please enter a valid HTTP or HTTPS URL.");
			}
		}

		private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			// Обновляем прогресс загрузки на ProgressBar
			progressBar.Value = e.ProgressPercentage;
		}

		private void urlTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			if (urlTextBox.Text == "Enter URL")
			{
				urlTextBox.Text = "";
				urlTextBox.Foreground = System.Windows.Media.Brushes.Black;
			}
		}

		private void urlTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(urlTextBox.Text))
			{
				urlTextBox.Text = "Enter URL";
				urlTextBox.Foreground = System.Windows.Media.Brushes.Gray;
			}
		}
	}
}
