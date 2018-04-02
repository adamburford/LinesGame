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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LinesGame.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Element current = new Element();

		public MainWindow()
		{
			InitializeComponent();
		}

		private void rect1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.current.InputElement = (IInputElement)sender;
		}

		private void rect2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.current.InputElement = (IInputElement)sender;
		}

		private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this.current.X = Mouse.GetPosition((IInputElement)sender).X;
			this.current.Y = Mouse.GetPosition((IInputElement)sender).Y;
			this.current.InputElement.CaptureMouse();
		}

		private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (this.current.InputElement != null)
			{
				this.current.IsDragging = false;
				this.current.InputElement.ReleaseMouseCapture();
				this.current.InputElement = null;
			}
		}

		private void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			// if mouse is down when its moving, then it's dragging current
			if (e.LeftButton == MouseButtonState.Pressed)
				this.current.IsDragging = true;


			if (this.current.IsDragging && current.InputElement != null)
			{

				// Retrieve the current position of the mouse.

				var newX = Mouse.GetPosition((IInputElement)sender).X;
				var newY = Mouse.GetPosition((IInputElement)sender).Y;


				// Reset the location of the object (add to sender's renderTransform

				// newPosition minus currentElement's position

				var rt = ((UIElement)this.current.InputElement).RenderTransform;
				var offsetX = rt.Value.OffsetX;
				var offsetY = rt.Value.OffsetY;
				rt.SetValue(TranslateTransform.XProperty, offsetX + newX - current.X);
				rt.SetValue(TranslateTransform.YProperty, offsetY + newY - current.Y);

				// Update position of the mouse
				current.X = newX;
				current.Y = newY;
			}
		}
	}

	public class Element
	{

		bool isDragging = false;
		IInputElement inputElement = null;
		double x, y = 0;

		public Element() { }

		public IInputElement InputElement
		{
			get { return this.inputElement; }
			set
			{
				this.inputElement = value;
				this.isDragging = false;
			}
		}

		public double X
		{
			get { return this.x; }
			set { this.x = value; }
		}

		public double Y
		{
			get { return this.y; }
			set { this.y = value; }
		}

		public bool IsDragging
		{
			get { return this.isDragging; }
			set { this.isDragging = value; }
		}
	}
}
