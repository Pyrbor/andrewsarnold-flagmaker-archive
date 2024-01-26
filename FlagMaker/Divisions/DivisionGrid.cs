using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FlagMaker.Divisions
{
	internal class DivisionGrid : Division
	{
		public DivisionGrid(Color color1, Color color2, int horizonalCount, int verticalCount)
			: base(new List<Color>
			{
				color1, color2
			}, new List<double>
			{
				horizonalCount, verticalCount
			})
		{
		}

		public override string Name { get { return "grid"; } }

		public override void Draw(Canvas canvas)
		{
			var background = new Rectangle
			{
				Fill = new SolidColorBrush(Colors[0]),
				Width = canvas.Width,
				Height = canvas.Height,
				SnapsToDevicePixels = true
			};
			canvas.Children.Add(background);
			Canvas.SetTop(background, 0);
			Canvas.SetLeft(background, 0);

			double height = canvas.Height / Values[1];
			double width = canvas.Width / Values[0];

			for (int x = 0; x < Values[0]; x++)
			{
				for (int y = 0; y < Values[1]; y++)
				{
					if ((x + y) % 2 == 0) continue;

					var rect = new Rectangle
								   {
									   Fill = new SolidColorBrush(Colors[1]),
									   Width = width,
									   Height = height,
									   SnapsToDevicePixels = true
								   };
					canvas.Children.Add(rect);
					Canvas.SetTop(rect, y * height);
					Canvas.SetLeft(rect, x * width);
				}
			}
		}

		public override void SetColors(List<Color> colors)
		{
			Colors[0] = colors[0];
			Colors[1] = colors[1];
		}

		public override void SetValues(List<double> values)
		{
			Values[0] = values[0];
			Values[1] = values[1];
		}

		public override string ExportSvg(int width, int height)
		{
			var sb = new StringBuilder();

			double h = height / Values[1];
			double w = width / Values[0];

			for (int x = 0; x < Values[0]; x++)
			{
				for (int y = 0; y < Values[1]; y++)
				{
					sb.Append(string.Format(CultureInfo.InvariantCulture, "<rect width=\"{0:0.###}\" height=\"{1:0.###}\" x=\"{2:0.###}\" y=\"{3:0.###}\" {4} />",
						w, h, x * w, y * h,
						((x + y) % 2 == 0 ? Colors[0] : Colors[1]).ToSvgFillWithOpacity()));
				}
			}

			return sb.ToString();
		}
	}
}