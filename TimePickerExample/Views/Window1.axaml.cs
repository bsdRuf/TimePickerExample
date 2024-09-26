using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using System.Threading.Tasks;
using System;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace TimePickerExample.Views
{
    public partial class Window1 : Window
    {
        private string hh { get; set; } = "";
        private string mm { get; set; } = "";
        private bool minutesTodo = false;

        public Window1()
        {
            InitializeComponent();
            this.DataContext = this;
            this.imgTimePicker.PointerPressed += imgTimePicker_PointerPressed;
            this.imgTimePicker.Source = new Bitmap(AssetLoader.Open(new Uri("avares://TimePickerExample/Assets/Hours.jpg")));
        }

        private void imgTimePicker_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            Avalonia.Controls.Image img = sender as Avalonia.Controls.Image;
            Point center = new Point(img.Height / 2, img.Width / 2);

            var point = e.GetCurrentPoint(sender as Control);
            var x = point.Position.X;
            var y = point.Position.Y;
            Point clickedPoint = new Point(x, y);

            this.GetAngleFromPoint(clickedPoint, center);
        }

        private async Task<int> GetAngleFromPoint(Point point, Point centerPoint)
        {
            #region calc angle
            double deltaX = point.X - centerPoint.X;
            double deltaY = point.Y - centerPoint.Y;
            int res = 0;

            if (deltaY.Equals(0))
            {
                return res;
            }

            double tan = deltaX / deltaY;
            double angle = Math.Atan(tan);

            // Convert to degrees.
            angle = angle * 180 / Math.PI;

            // If the mouse crosses the vertical center,
            // find the complementary angle.
            if (deltaY > 0)
            {
                angle = 180 - Math.Abs(angle);
            }

            // Rotate left if the mouse moves left and right
            // if the mouse moves right.
            if (deltaX < 0)
            {
                angle = -Math.Abs(angle);
            }
            else
            {
                angle = Math.Abs(angle);
            }

            if (Double.IsNaN(angle))
            {
                return res;
            }
            #endregion

            if (this.minutesTodo)
            {
                // left side -180
                if (angle < 0)
                {
                    res = (int)Math.Round((angle + 360) / 6);
                    if (res < 10)
                    {
                        mm = "0" + res;
                    }
                    else
                    {
                        if (res == 60)
                        {
                            mm = "00";
                        }
                        else
                        {
                            mm = "" + res;
                        }
                    }
                }
                else // right side +180
                {
                    res = (int)Math.Round((angle / 6));
                    if (res < 10)
                    {
                        mm = "0" + res;
                    }
                    else
                    {
                        mm = "" + res;
                    }
                }

                string message = "Time: " + hh + ":" + mm;
                this.lblSelectedTime.Content = message;

                this.minutesTodo = false;
                this.imgTimePicker.Source = new Bitmap(AssetLoader.Open(new Uri("avares://TimePickerExample/Assets/Hours.jpg")));
            }
            else
            {
                if (angle < 0)
                {
                    res = (int)Math.Round((angle + 360) / 15);
                    if (res < 10)
                    {
                        hh = "0" + res;
                    }
                    else
                    {
                        hh = "" + res;
                    }
                }
                else
                {
                    res = (int)Math.Round((angle / 15));
                    if (res < 10)
                    {
                        hh = "0" + res;
                    }
                    else
                    {
                        hh = "" + res;
                    }
                }
                FlyoutBase.ShowAttachedFlyout(this.myButton);
                this.minutesTodo = true;
                this.imgTimePicker.Source = new Bitmap(AssetLoader.Open(new Uri("avares://TimePickerExample/Assets/Minutes.jpg")));
            }

            return res;
        }

        public async void FlyoutBtnClick(object? sender, RoutedEventArgs args)
        {
            var ctl = sender as Control;
            if (ctl != null)
            {
                FlyoutBase.ShowAttachedFlyout(ctl);
            }

            //erstes öffnen
            this.minutesTodo = false;
        }
    }
}