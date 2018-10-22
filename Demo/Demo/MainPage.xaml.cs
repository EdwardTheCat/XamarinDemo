using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo
{
    public partial class MainPage : ContentPage
    {
        public MyData myDataObject = new MyData();
        int step;

        public class MyData : INotifyPropertyChanged
        {
            private int x=0;

            public MyData() { }
            
            public int X
            {
                get { return x; }
                set
                {
                    x = value;
                    OnPropertyChanged("X");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropertyChanged(string info)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(info));
                }
            }
        }

        public MainPage()
        {
            InitializeComponent();

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    step = 20;
                    break;
                case Device.Android:
                    step = 50;
                    break;

                case Device.UWP:
                    step = 100;
                    break;
                    
                default:
                    step = 1;
                    break;
            }

            Label label = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Button buttonUp = new Button
            {
                Text = "UP",
                VerticalOptions = LayoutOptions.End
            };

            Button buttonDown = new Button
            {
                Text = "DOWN",
                VerticalOptions = LayoutOptions.End
            };

            StackLayout layout= new StackLayout
            {
                Spacing = 0,
                Orientation = StackOrientation.Vertical,

            };
            layout.Children.Add(label);
            layout.Children.Add(buttonUp);
            layout.Children.Add(buttonDown);

            buttonUp.Clicked += OnButtonUpClicked;
            buttonDown.Clicked += OnButtonDownClicked;
            // Binding between myDataObject source and label target
            label.BindingContext = myDataObject;
            // Bind the new data source to the text property.
            label.SetBinding(Label.TextProperty, "X");

            Content = layout;
        }

        void OnButtonUpClicked(object sender, EventArgs args)
        {
            myDataObject.X += step;

        }
        void OnButtonDownClicked(object sender, EventArgs args)
        {
            myDataObject.X -= step;

        }
    }
}
