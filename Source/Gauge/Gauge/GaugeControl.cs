using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace Gauge
{
    /// <summary>
    /// Creates a gauge image.
    /// </summary>
    /// <remarks>
    /// Adapted from http://www.ucancode.net/Visual_C_MFC_Samples/CSharp_Example_Free_DOTNET_Gauge_Control_Draw_Source_Code.htm
    /// </remarks>
    public class GaugeControl
    {
        #region Private Attributes
        /// <summary>
        /// Dimensions for drawing.
        /// </summary>
        int _x, _y, _width, _height;

        /// <summary>
        /// The starting angle for the gauge.
        /// </summary>
        float _fromAngle = 135F;

        /// <summary>
        /// The ending angle for the gauge: 45 degrees past 0 (3 o'clock).
        /// </summary>
        float _toAngle = 405F;
        #endregion

        /// <summary>
        /// This class creates a gauge image.
        /// </summary>
        public GaugeControl()
        {
            // Initialize default values.
            AdjustLabelSize = true;
            BackColor = Color.Transparent.ToArgb();
            BackColor2 = BackColor;
            Band1Color = Color.FromArgb(240, 240, 240).ToArgb();
            Band1End = 30;
            Band2Color = Color.FromArgb(255, 255, 160).ToArgb();
            Band2End = 70;
            Band3Color = Color.FromArgb(255, 160, 160).ToArgb();
            DialColor = Color.Lavender.ToArgb();
            DialTextColor = Color.Black.ToArgb();
            DigitsColor = Color.Black.ToArgb();
            DisplayDigitalValue = false;
            LabelFontName = "Tahoma";
            LabelFontSize = 10;
            DialTextFontName = "Tahoma";
            DialTextFontSize = 10;
            Format = "{0:#,##0}";
            Glossiness = 75;
            GoalMarkerColor = Color.Purple.ToArgb();
            GoalPosition = 100;
            LabelColor = Color.Black.ToArgb();
            LabelDistance = 0;
            LabelFactor = 1;
            MajorTickColor = Color.Black.ToArgb();
            MajorTicks = 10;
            MaxValue = 100;
            MinorTickColor = Color.Black.ToArgb();
            MinorTicks = 3;
            MinValue = 0;
            Value = 0;
        }

        #region Public Properties
        /// <summary>
        /// Automatically adjust the label size based on the gauge size.
        /// </summary>
        public bool AdjustLabelSize { get; set; }

        /// <summary>
        /// The background color of the image.
        /// </summary>
        private Color backColor;

        /// <summary>
        /// The underlying store for BackColor.
        /// </summary>
        private int _backColor;

        /// <summary>
        /// The background color of the image as an ARGB value.
        /// </summary>
        public int BackColor
        {
            get
            {
                return _backColor;
            }
            set
            {
                _backColor = value;
                backColor = Color.FromArgb(value);
            }
        }

        /// <summary>
        /// The end color of a background gradient.
        /// </summary>
        private Color backColor2;

        /// <summary>
        /// The underlying store for BackColor2.
        /// </summary>
        private int _backColor2;

        /// <summary>
        /// The of a background gradient as an ARGB value.
        /// </summary>
        public int BackColor2
        {
            get
            {
                return _backColor2;
            }
            set
            {
                _backColor2 = value;
                backColor2 = Color.FromArgb(value);
            }
        }

        /// <summary>
        /// The mode for a background gradient.
        /// </summary>
        public int BackGradientMode { get; set; }

        /// <summary>
        /// The color of band 1.
        /// </summary>
        private Color band1Color;

        /// <summary>
        /// The underlying store for Band1Color.
        /// </summary>
        private int _band1Color;

        /// <summary>
        /// The color of band 1 as an ARGB value.
        /// </summary>
        public int Band1Color
        {
            get
            {
                return _band1Color;
            }
            set
            {
                _band1Color = value;
                band1Color = Color.FromArgb(value);
            }
        }

        /// <summary>
        /// The end of band 1 as a percentage.
        /// </summary>
        public int Band1End { get; set; }

        /// <summary>
        /// The color of band 2.
        /// </summary>
        private Color band2Color;

        /// <summary>
        /// The underlying store for Band2Color.
        /// </summary>
        private int _band2Color;

        /// <summary>
        /// The color of band 2 as an ARGB value.
        /// </summary>
        public int Band2Color
        {
            get
            {
                return _band2Color;
            }
            set
            {
                _band2Color = value;
                band2Color = Color.FromArgb(value);
            }
        }

        /// <summary>
        /// The end of band 2 as a percentage.
        /// </summary>
        public int Band2End { get; set; }

        /// <summary>
        /// The color of band 3.
        /// </summary>
        private Color band3Color;

        /// <summary>
        /// The underlying store for Band3Color.
        /// </summary>
        private int _band3Color;

        /// <summary>
        /// The color of band 3 as an ARGB value.
        /// </summary>
        public int Band3Color
        {
            get
            {
                return _band3Color;
            }
            set
            {
                _band3Color = value;
                band3Color = Color.FromArgb(value);
            }
        }

        /// <summary>
        /// The background color of the dial.
        /// </summary>
        private Color dialColor;

        /// <summary>
        /// The underlying store for DialColor.
        /// </summary>
        private int _dialColor;

        /// <summary>
        /// The background color of the dial as an ARGB value.
        /// </summary>
        public int DialColor
        {
            get
            {
                return _dialColor;
            }
            set
            {
                _dialColor = value;
                dialColor = Color.FromArgb(value);
            }
        }

        /// <summary>
        /// The text to be displayed in the dial.
        /// </summary>
        public string DialText { get; set; }

        /// <summary>
        /// The dial text color.
        /// </summary>
        private Color dialTextColor;

        /// <summary>
        /// The underlying store for DialTextColor.
        /// </summary>
        private int _dialTextColor;

        /// <summary>
        /// The dial text color as an ARGB value.
        /// </summary>
        public int DialTextColor
        {
            get
            {
                return _dialTextColor;
            }
            set
            {
                _dialTextColor = value;
                dialTextColor = Color.FromArgb(value);
            }
        }

        /// <summary>
        /// True to use bold for labels.
        /// </summary>
        public bool DialTextFontBold { get; set; }

        /// <summary>
        /// True to use italics for labels.
        /// </summary>
        public bool DialTextFontItalic { get; set; }

        /// <summary>
        /// The font name for labels.
        /// </summary>
        public string DialTextFontName { get; set; }

        /// <summary>
        /// The font size for labels.
        /// </summary>
        public int DialTextFontSize { get; set; }

        /// <summary>
        /// The color of decimal digits.
        /// </summary>
        private Color digitsColor;

        /// <summary>
        /// The underlying store for DigitsColor.
        /// </summary>
        private int _digitsColor;

        /// <summary>
        /// The color of decimal digits as an ARGB value.
        /// </summary>
        public int DigitsColor
        {
            get
            {
                return _digitsColor;
            }
            set
            {
                _digitsColor = value;
                digitsColor = Color.FromArgb(value);
            }
        }

        /// <summary>
        /// True to display a digital value.
        /// </summary>
        public bool DisplayDigitalValue { get; set; }

        /// <summary>
        /// The name of the image file to create.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// True to use bold for labels.
        /// </summary>
        public bool LabelFontBold { get; set; }

        /// <summary>
        /// True to use italics for labels.
        /// </summary>
        public bool LabelFontItalic { get; set; }

        /// <summary>
        /// The font name for labels.
        /// </summary>
        public string LabelFontName { get; set; }

        /// <summary>
        /// The font size for labels.
        /// </summary>
        public int LabelFontSize { get; set; }

        /// <summary>
        /// The format to use for labels.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// The alpha to use for the "glossy" part of the image.
        /// </summary>
        private float _glossinessAlpha;

        /// <summary>
        /// Glossiness strength. Range: 0-100
        /// </summary>
        public float Glossiness
        {
            get
            {
                return (_glossinessAlpha * 100) / 220;
            }
            set
            {
                float val = value;
                if (val > 100)
                {
                    value = 100;
                }
                else if (val < 0)
                {
                    value = 0;
                }
                _glossinessAlpha = (value * 220) / 100;
            }
        }

        /// <summary>
        /// The color of the goal marker.
        /// </summary>
        private Color goalMarkerColor;

        /// <summary>
        /// The underlying store for GoalMarkerColor.
        /// </summary>
        private int _goalMarkerColor;

        /// <summary>
        /// The color of the goal marker as an ARGB value.
        /// </summary>
        public int GoalMarkerColor
        {
            get
            {
                return _goalMarkerColor;
            }
            set
            {
                _goalMarkerColor = value;
                goalMarkerColor = Color.FromArgb(value);
            }
        }

        /// <summary>
        /// Goal position on the scale.
        /// </summary>
        public float GoalPosition { get; set; }

        /// <summary>
        /// The gauge height (and width, since it's a square).
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The color of labels.
        /// </summary>
        private Color labelColor;

        /// <summary>
        /// The underlying store for LabelColor.
        /// </summary>
        private int _labelColor;

        /// <summary>
        /// The color of labels as an ARGB value.
        /// </summary>
        public int LabelColor
        {
            get
            {
                return _labelColor;
            }
            set
            {
                _labelColor = value;
                labelColor = Color.FromArgb(value);
            }
        }

        /// <summary>
        /// The distance between labels and major tick marks.
        /// </summary>
        public int LabelDistance { get; set; }

        private int _labelFactor;

        /// <summary>
        /// A factor to divide label values by so large numbers can be minimized.
        /// </summary>
        public int LabelFactor
        {
            get
            {
                return _labelFactor;
            }
            set
            {
                if (value > 0)
                {
                    _labelFactor = value;
                }
            }
        }

        /// <summary>
        /// The color of major ticks.
        /// </summary>
        private Color majorTickColor;

        /// <summary>
        /// The underlying store for MajorTickColor.
        /// </summary>
        private int _majorTickColor;

        /// <summary>
        /// The color of major ticks as an ARGB value.
        /// </summary>
        public int MajorTickColor
        {
            get
            {
                return _majorTickColor;
            }
            set
            {
                _majorTickColor = value;
                majorTickColor = Color.FromArgb(value);
            }
        }

        /// <summary>
        /// The underlying store for MajorTicks.
        /// </summary>
        private int _majorTicks;

        /// <summary>
        /// The number of major ticks in the dial scale.
        /// </summary>
        public int MajorTicks
        {
            get
            {
                return _majorTicks;
            }
            set
            {
                if (value > 1 && value < 25)
                {
                    _majorTicks = value;
                }
            }
        }

        /// <summary>
        /// Maximum value on the scale
        /// </summary>
        public float MaxValue { get; set; }

        /// <summary>
        /// The color of minor ticks.
        /// </summary>
        private Color minorTickColor;

        /// <summary>
        /// The underlying store for MinorTickColor.
        /// </summary>
        private int _minorTickColor;

        /// <summary>
        /// The color of minor ticks as an ARGB value.
        /// </summary>
        public int MinorTickColor
        {
            get
            {
                return _minorTickColor;
            }
            set
            {
                _minorTickColor = value;
                minorTickColor = Color.FromArgb(value);
            }
        }

        /// <summary>
        /// The underlying store for MinorTicks.
        /// </summary>
        private int _minorTicks;

        /// <summary>
        /// The number of minor ticks between major ticks.
        /// </summary>
        public int MinorTicks
        {
            get
            {
                return _minorTicks;
            }
            set
            {
                if (value >= 0 && value <= 10)
                {
                    _minorTicks = value;
                }
            }
        }

        /// <summary>
        /// Mininum value on the scale
        /// </summary>
        public float MinValue { get; set; }

        /// <summary>
        /// True to display a marker for the goal value.
        /// </summary>
        public bool ShowGoalMarker { get; set; }

        /// <summary>
        /// Value where the pointer will point to.
        /// </summary>
        public float Value { get; set; }
        #endregion

        /// <summary>
        /// Draws the gauge.
        /// </summary>
        public string DrawGauge()
        {
            // Create the image and graphics objects.
            int height = Height;
            int width = Height;
            Image backgroundImg = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(backgroundImg);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // Draw the background.
            Brush brush;
            Rectangle background = new Rectangle(0, 0, width, height);
            if (backColor == backColor2)
            {
                brush = new SolidBrush(backColor);
            }
            else
            {
                brush = new LinearGradientBrush(background, backColor, backColor2, (LinearGradientMode)BackGradientMode);
            }
            g.FillRectangle(brush, new Rectangle(0, 0, width, height));

            // Determines the drawing dimensions.
            _x = (int)(width * 0.05);
            _y = (int)(height * 0.05);
            _width = width - _x * 2;
            _height = height - _y * 2;
            Rectangle rectImg = new Rectangle(_x, _y, _width, _height);

            // Draw the dial background.
            Brush backGroundBrush = new SolidBrush(dialColor);
            g.FillEllipse(backGroundBrush, _x, _y, _width, _height);

            // Draw the rim.
            // TODO: properties for outer rim color, width, and transparency
            SolidBrush outlineBrush = new SolidBrush(Color.FromArgb(100, Color.SlateGray));
            Pen outline = new Pen(outlineBrush, (float)(_width * .03));
            g.DrawEllipse(outline, rectImg);
            // TODO: properties for inner rim color and width
            Pen darkRim = new Pen(Color.SlateGray);
            g.DrawEllipse(darkRim, _x, _y, _width, _height);

            // Draw the tick marks and labels.
            DrawCalibration(g, rectImg, ((_width) / 2) + _x, ((_height) / 2) + _y, width);

            // Draw band 1
            // TODO: properties for band alpha
            Pen colorPen = new Pen(Color.FromArgb(255, band1Color), width / 40);
            int gap = (int)(width * 0.03F);
            Rectangle rectg = new Rectangle(rectImg.X + gap, rectImg.Y + gap, rectImg.Width - gap * 2, rectImg.Height - gap * 2);
            float startAngle = _fromAngle;
            float sweepAngle = (_toAngle - _fromAngle) * Band1End / 100;
            g.DrawArc(colorPen, rectg, startAngle, sweepAngle);

            // Draw band 2
            startAngle += sweepAngle;
            colorPen = new Pen(Color.FromArgb(255, band2Color), width / 40);
            sweepAngle = (_toAngle - _fromAngle) * (Band2End - Band1End) / 100;
            g.DrawArc(colorPen, rectg, startAngle, sweepAngle);

            // Draw band 3
            startAngle += sweepAngle;
            colorPen = new Pen(Color.FromArgb(255, band3Color), width / 40);
            sweepAngle = (_toAngle - _fromAngle) * (100 - Band2End) / 100;
            g.DrawArc(colorPen, rectg, startAngle, sweepAngle);

            // Draw the digital value.
            if (DisplayDigitalValue)
            {
                RectangleF digiRect = new RectangleF((float)width / 2F - (float)width / 5F, (float)height / 1.3F,
                    (float)width / 2.5F, (float)height / 9F);
                RectangleF digiFRect = new RectangleF(width / 2 - width / 5.3F, (int)(height / 1.28), width / 4, height / 12);
                g.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Gray)), digiRect);
                DisplayNumber(g, Value, digiFRect);
            }

            // Draw the dial text.
            if (!String.IsNullOrEmpty(DialText))
            {
                FontStyle style = FontStyle.Regular;
                if (DialTextFontBold)
                {
                    style = style | FontStyle.Bold;
                }
                if (DialTextFontItalic)
                {
                    style = style | FontStyle.Italic;
                }
                Font font = new Font(DialTextFontName, DialTextFontSize, style);

                SizeF textSize = g.MeasureString(DialText, font);
                RectangleF digiFRectText = new RectangleF(width / 2 - textSize.Width / 2, (int)(_height / 1.5),
                    (float)Math.Ceiling(textSize.Width), textSize.Height);
                g.DrawString(DialText, font, new SolidBrush(dialTextColor), digiFRectText);
            }

            // Draw the pointer.
            g.SmoothingMode = SmoothingMode.AntiAlias;
            _width = width - _x * 2;
            _height = height - _y * 2;
            DrawPointer(g, ((_width) / 2) + _x, ((_height) / 2) + _y, width, height);

            // Draw the goal marker.
            if (ShowGoalMarker && GoalPosition >= MinValue && GoalPosition <= MaxValue)
            {
                DrawGoalMarker(g, rectImg, ((_width) / 2) + _x, ((_height) / 2) + _y, width);
            }

            // Return the image as a string.
            Stream stream = new MemoryStream();
            backgroundImg.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            stream.Position = 0;
            string results;
            using (StreamReader reader = new StreamReader(stream, Encoding.Default))
            {
                results = reader.ReadToEnd();
            }
            return results;
        }

        #region Private methods
        /// <summary>
        /// Draws the pointer.
        /// </summary>
        /// <param name="gr">
        /// The graphics object to draw on.
        /// </param>
        /// <param name="cx">
        /// The X value of the location to draw at.
        /// </param>
        /// <param name="cy">
        /// The Y value of the location to draw at.
        /// </param>
        /// <param name="width">
        /// The width to use.
        /// </param>
        /// <param name="height">
        /// The height to use.
        /// </param>
        private void DrawPointer(Graphics gr, int cx, int cy, int width, int height)
        {
            float radius = width / 2 - (width * .12F);
            float val = Math.Abs(MaxValue - MinValue);

            Image img = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(img);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (MaxValue > MinValue)
            {
                val = (100 * Math.Max(Math.Min(Value, MaxValue) - MinValue, MinValue)) / val;
            }
            else
            {
                val = 100 - (100 * Math.Max(Math.Min(Value, MinValue) - MaxValue, MaxValue)) / val;
            }
            val = ((_toAngle - _fromAngle) * val) / 100;
            val += _fromAngle;

            float angle = GetRadian(val);
            float gradientAngle = angle;

            PointF[] pts = new PointF[5];

            pts[0].X = (float)(cx + radius * Math.Cos(angle));
            pts[0].Y = (float)(cy + radius * Math.Sin(angle));

            pts[4].X = (float)(cx + radius * Math.Cos(angle - 0.02));
            pts[4].Y = (float)(cy + radius * Math.Sin(angle - 0.02));

            angle = GetRadian((val + 20));
            pts[1].X = (float)(cx + (width * .09F) * Math.Cos(angle));
            pts[1].Y = (float)(cy + (width * .09F) * Math.Sin(angle));

            pts[2].X = cx;
            pts[2].Y = cy;

            angle = GetRadian((val - 20));
            pts[3].X = (float)(cx + (width * .09F) * Math.Cos(angle));
            pts[3].Y = (float)(cy + (width * .09F) * Math.Sin(angle));

            // TODO: properties for pointer color
            Brush pointer = new SolidBrush(Color.Black);
            g.FillPolygon(pointer, pts);

            PointF[] shinePts = new PointF[3];
            angle = GetRadian(val);
            shinePts[0].X = (float)(cx + radius * Math.Cos(angle));
            shinePts[0].Y = (float)(cy + radius * Math.Sin(angle));

            angle = GetRadian(val + 20);
            shinePts[1].X = (float)(cx + (width * .09F) * Math.Cos(angle));
            shinePts[1].Y = (float)(cy + (width * .09F) * Math.Sin(angle));

            shinePts[2].X = cx;
            shinePts[2].Y = cy;

            // TODO: properties for pointer color
            LinearGradientBrush gpointer = new LinearGradientBrush(shinePts[0], shinePts[2], Color.SlateGray, Color.Black);
            g.FillPolygon(gpointer, shinePts);

            Rectangle rect = new Rectangle(_x, _y, _width, _height);
            DrawCenterPoint(g, rect, ((_width) / 2) + _x, ((_height) / 2) + _y, width);

            DrawGloss(g);

            gr.DrawImage(img, 0, 0);
        }

        /// <summary>
        /// Draws the glossy part of the gauge.
        /// </summary>
        /// <param name="g">
        /// The graphics object.
        /// </param>
        private void DrawGloss(Graphics g)
        {
            RectangleF glossRect = new RectangleF(
               _x + (float)(_width * 0.10),
               _y + (float)(_height * 0.07),
               (float)(_width * 0.80),
               (float)(_height * 0.7));
            LinearGradientBrush gradientBrush = new LinearGradientBrush(glossRect,
                Color.FromArgb((int)_glossinessAlpha, Color.White),
                Color.Transparent,
                LinearGradientMode.Vertical);
            g.FillEllipse(gradientBrush, glossRect);

            // TODO: this draws the bottom "glossy" ellipse but it looks odd when a small alpha
            // value is used for the background so we won't use it for now.
            /*
            glossRect = new RectangleF(
               _x + (float)(_width * 0.25),
               _y + (float)(_height * 0.77),
               (float)(_width * 0.50),
               (float)(_height * 0.2));
            int gloss = (int)(_glossinessAlpha / 3);
            gradientBrush = new LinearGradientBrush(glossRect,
                Color.Transparent,
                Color.FromArgb(gloss, backColor),
                LinearGradientMode.Vertical);
            g.FillEllipse(gradientBrush, glossRect);
            */
        }

        /// <summary>
        /// Draws the center point.
        /// </summary>
        /// <param name="g">
        /// The graphics object to draw on.
        /// </param>
        /// <param name="rect">
        /// The drawing rectangle.
        /// </param>
        /// <param name="cX">
        /// The X value of the location to draw at.
        /// </param>
        /// <param name="cY">
        /// The Y value of the location to draw at.
        /// </param>
        /// <param name="width">
        /// The width to use.
        /// </param>
        private void DrawCenterPoint(Graphics g, Rectangle rect, int cX, int cY, int width)
        {
            float shift = width / 5;
            RectangleF rectangle = new RectangleF(cX - (shift / 2), cY - (shift / 2), shift, shift);
            // TODO: properties for center point colors
            LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Black, Color.FromArgb(100, dialColor),
                LinearGradientMode.Vertical);
            g.FillEllipse(brush, rectangle);

            shift = width / 7;
            rectangle = new RectangleF(cX - (shift / 2), cY - (shift / 2), shift, shift);
            // TODO: properties for center point colors
            brush = new LinearGradientBrush(rect, Color.SlateGray, Color.Black, LinearGradientMode.ForwardDiagonal);
            g.FillEllipse(brush, rectangle);
        }

        /// <summary>
        /// Draws the goal marker.
        /// </summary>
        /// <param name="g">
        /// The graphics object to draw on.
        /// </param>
        /// <param name="rect">
        /// The drawing rectangle.
        /// </param>
        /// <param name="cX">
        /// The X value of the location to draw at.
        /// </param>
        /// <param name="cY">
        /// The Y value of the location to draw at.
        /// </param>
        /// <param name="width">
        /// The width to use.
        /// </param>
        private void DrawGoalMarker(Graphics g, Rectangle rect, int cX, int cY, int width)
        {
            int gap = (int)(width * 0.01F);
            Rectangle rectangle = new Rectangle(rect.Left + gap, rect.Top + gap, rect.Width - gap, rect.Height - gap);

            float radius = rectangle.Width / 2 - gap * 5;
            float totalAngle = _toAngle - _fromAngle;
            float angle = GetRadian(totalAngle * GoalPosition / MaxValue + _fromAngle);

            // Draw a diamond.
            PointF[] pts = new PointF[4];
            pts[0].X = (float)(cX + radius * Math.Cos(angle));
            pts[0].Y = (float)(cY + radius * Math.Sin(angle));

            float depth = radius + width * 0.02F;
            pts[1].X = (float)(cX + depth * Math.Cos(angle - 0.04));
            pts[1].Y = (float)(cY + depth * Math.Sin(angle - 0.04));

            pts[3].X = (float)(cX + depth * Math.Cos(angle + 0.04));
            pts[3].Y = (float)(cY + depth * Math.Sin(angle + 0.04));

            depth = radius + width * 0.04F;
            pts[2].X = (float)(cX + depth * Math.Cos(angle));
            pts[2].Y = (float)(cY + depth * Math.Sin(angle));

            Brush marker = new SolidBrush(goalMarkerColor);
            g.FillPolygon(marker, pts);
        }

        /// <summary>
        /// Draws the tick marks and labels.
        /// </summary>
        /// <param name="g">
        /// The graphics object to draw on.
        /// </param>
        /// <param name="rect">
        /// The drawing rectangle.
        /// </param>
        /// <param name="cX">
        /// The X value of the location to draw at.
        /// </param>
        /// <param name="cY">
        /// The Y value of the location to draw at.
        /// </param>
        /// <param name="width">
        /// The width to use.
        /// </param>
        private void DrawCalibration(Graphics g, Rectangle rect, int cX, int cY, int width)
        {
            int noOfParts = MajorTicks + 1;
            int noOfIntermediates = MinorTicks;
            float currentAngle = GetRadian(_fromAngle);
            int gap = (int)(width * 0.01F);
            float shift = width / 25;
            Rectangle rectangle = new Rectangle(rect.Left + gap, rect.Top + gap, rect.Width - gap, rect.Height - gap);

            float x, y, x1, y1, tx, ty, radius;
            radius = rectangle.Width / 2 - gap * 5;
            float totalAngle = _toAngle - _fromAngle;
            float incr = GetRadian(((totalAngle) / ((noOfParts - 1) * (noOfIntermediates + 1))));

            FontStyle style = FontStyle.Regular;
            if (LabelFontBold)
            {
                style = style | FontStyle.Bold;
            }
            if (LabelFontItalic)
            {
                style = style | FontStyle.Italic;
            }
            Font font = new Font(LabelFontName, LabelFontSize, style);

            Pen thickPen = new Pen(majorTickColor, width / 50);
            Pen thinPen = new Pen(minorTickColor, width / 100);
            Brush stringPen = new SolidBrush(labelColor);
            Font f;
            if (AdjustLabelSize)
            {
                f = new Font(font.FontFamily, (float)(width / 23), font.Style);
            }
            else
            {
                f = font;
            }
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            format.Alignment = StringAlignment.Center;
            float rulerValue = MinValue;
            float rulerIncrement = (MaxValue - MinValue) / (noOfParts - 1);
            for (int i = 0; i < noOfParts; i++)
            {
                // Draw a major tick.
                double cos = Math.Cos(currentAngle);
                double sin = Math.Sin(currentAngle);
                x = (float)(cX + radius * cos);
                y = (float)(cY + radius * sin);
                x1 = (float)(cX + (radius - width / 20) * cos);
                y1 = (float)(cY + (radius - width / 20) * sin);
                g.DrawLine(thickPen, x, y, x1, y1);

                // Draw the labels.
                tx = (float)(cX + (radius - LabelDistance - width / 10) * cos);
                ty = (float)(cY - shift + (radius - LabelDistance - width / 10) * sin);
                g.DrawString(String.Format(Format, rulerValue / LabelFactor), f, stringPen, new PointF(tx, ty), format);

                // Move to the next position.
                rulerValue += rulerIncrement;

                // If we're not on the last position on the dial, draw minor ticks.
                if (i < noOfParts - 1)
                {
                    // Draw minor ticks if we're supposed to.
                    if (noOfIntermediates > 0)
                    {
                        for (int j = 0; j <= noOfIntermediates; j++)
                        {
                            currentAngle += incr;
                            cos = Math.Cos(currentAngle);
                            sin = Math.Sin(currentAngle);
                            x = (float)(cX + radius * cos);
                            y = (float)(cY + radius * sin);
                            x1 = (float)(cX + (radius - width / 50) * cos);
                            y1 = (float)(cY + (radius - width / 50) * sin);
                            g.DrawLine(thinPen, x, y, x1, y1);
                        }
                    }

                    // We're not doing minor ticks, so move to the next position on the dial.
                    else
                    {
                        currentAngle += incr;
                    }
                }
            }
        }

        /// <summary>
        /// Converts the given value from degrees to radians.
        /// </summary>
        /// <param name="theta">
        /// The value in degrees.
        /// </param>
        /// <returns>
        /// The value in radians.
        /// </returns>
        public float GetRadian(float theta)
        {
            return theta * (float)Math.PI / 180F;
        }

        /// <summary>
        /// Displays the given number in the 7-Segment format.
        /// </summary>
        /// <param name="g">
        /// The graphics object to draw on.
        /// </param>
        /// <param name="number">
        /// The number to display.
        /// </param>
        /// <param name="drect">
        /// The drawing rectangle.
        /// </param>
        private void DisplayNumber(Graphics g, float number, RectangleF drect)
        {
            try
            {
                string num = String.Format(Format.Replace("$", "").Replace(",", ""), number).PadLeft(7, ' ');
                float shift = 0;
                if (number < 0)
                {
                    shift -= _width / 17;
                }
                bool drawDPS = false;
                char[] chars = num.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    char c = chars[i];
                    drawDPS = i < chars.Length - 1 && chars[i + 1] == '.';
                    if (c != '.')
                    {
                        if (c == '-')
                        {
                            DrawDigit(g, -1, new PointF(drect.X + shift, drect.Y), drawDPS, drect.Height);
                        }
                        else if (c != ' ')
                        {
                            DrawDigit(g, Int16.Parse(c.ToString()), new PointF(drect.X + shift, drect.Y), drawDPS, drect.Height);
                        }
                        shift += 15 * _width / 250;
                    }
                    else
                    {
                        shift += 2 * _width / 250;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Draws a digit in 7-Segment format.
        /// </summary>
        /// <param name="g">
        /// The graphics object to draw on.
        /// </param>
        /// <param name="number">
        /// The digit to draw.
        /// </param>
        /// <param name="position">
        /// The position to draw it at.
        /// </param>
        /// <param name="dp">
        /// True to draw a decimal point.
        /// </param>
        /// <param name="height">
        /// The height to draw.
        /// </param>
        private void DrawDigit(Graphics g, int number, PointF position, bool dp, float height)
        {
            float width;
            width = 10F * height / 13;

            Pen outline = new Pen(Color.FromArgb(40, dialColor));
            Pen fillPen = new Pen(digitsColor);

            #region Form Polygon Points
            //Segment A
            PointF[] segmentA = new PointF[5];
            segmentA[0] = segmentA[4] = new PointF(position.X + GetX(2.8F, width), position.Y + GetY(1F, height));
            segmentA[1] = new PointF(position.X + GetX(10, width), position.Y + GetY(1F, height));
            segmentA[2] = new PointF(position.X + GetX(8.8F, width), position.Y + GetY(2F, height));
            segmentA[3] = new PointF(position.X + GetX(3.8F, width), position.Y + GetY(2F, height));

            //Segment B
            PointF[] segmentB = new PointF[5];
            segmentB[0] = segmentB[4] = new PointF(position.X + GetX(10, width), position.Y + GetY(1.4F, height));
            segmentB[1] = new PointF(position.X + GetX(9.3F, width), position.Y + GetY(6.8F, height));
            segmentB[2] = new PointF(position.X + GetX(8.4F, width), position.Y + GetY(6.4F, height));
            segmentB[3] = new PointF(position.X + GetX(9F, width), position.Y + GetY(2.2F, height));

            //Segment C
            PointF[] segmentC = new PointF[5];
            segmentC[0] = segmentC[4] = new PointF(position.X + GetX(9.2F, width), position.Y + GetY(7.2F, height));
            segmentC[1] = new PointF(position.X + GetX(8.7F, width), position.Y + GetY(12.7F, height));
            segmentC[2] = new PointF(position.X + GetX(7.6F, width), position.Y + GetY(11.9F, height));
            segmentC[3] = new PointF(position.X + GetX(8.2F, width), position.Y + GetY(7.7F, height));

            //Segment D
            PointF[] segmentD = new PointF[5];
            segmentD[0] = segmentD[4] = new PointF(position.X + GetX(7.4F, width), position.Y + GetY(12.1F, height));
            segmentD[1] = new PointF(position.X + GetX(8.4F, width), position.Y + GetY(13F, height));
            segmentD[2] = new PointF(position.X + GetX(1.3F, width), position.Y + GetY(13F, height));
            segmentD[3] = new PointF(position.X + GetX(2.2F, width), position.Y + GetY(12.1F, height));

            //Segment E
            PointF[] segmentE = new PointF[5];
            segmentE[0] = segmentE[4] = new PointF(position.X + GetX(2.2F, width), position.Y + GetY(11.8F, height));
            segmentE[1] = new PointF(position.X + GetX(1F, width), position.Y + GetY(12.7F, height));
            segmentE[2] = new PointF(position.X + GetX(1.7F, width), position.Y + GetY(7.2F, height));
            segmentE[3] = new PointF(position.X + GetX(2.8F, width), position.Y + GetY(7.7F, height));

            //Segment F
            PointF[] segmentF = new PointF[5];
            segmentF[0] = segmentF[4] = new PointF(position.X + GetX(3F, width), position.Y + GetY(6.4F, height));
            segmentF[1] = new PointF(position.X + GetX(1.8F, width), position.Y + GetY(6.8F, height));
            segmentF[2] = new PointF(position.X + GetX(2.6F, width), position.Y + GetY(1.3F, height));
            segmentF[3] = new PointF(position.X + GetX(3.6F, width), position.Y + GetY(2.2F, height));

            //Segment G
            PointF[] segmentG = new PointF[7];
            segmentG[0] = segmentG[6] = new PointF(position.X + GetX(2F, width), position.Y + GetY(7F, height));
            segmentG[1] = new PointF(position.X + GetX(3.1F, width), position.Y + GetY(6.5F, height));
            segmentG[2] = new PointF(position.X + GetX(8.3F, width), position.Y + GetY(6.5F, height));
            segmentG[3] = new PointF(position.X + GetX(9F, width), position.Y + GetY(7F, height));
            segmentG[4] = new PointF(position.X + GetX(8.2F, width), position.Y + GetY(7.5F, height));
            segmentG[5] = new PointF(position.X + GetX(2.9F, width), position.Y + GetY(7.5F, height));

            //Segment DP
            #endregion

            #region Draw Segments Outline
            g.FillPolygon(outline.Brush, segmentA);
            g.FillPolygon(outline.Brush, segmentB);
            g.FillPolygon(outline.Brush, segmentC);
            g.FillPolygon(outline.Brush, segmentD);
            g.FillPolygon(outline.Brush, segmentE);
            g.FillPolygon(outline.Brush, segmentF);
            g.FillPolygon(outline.Brush, segmentG);
            #endregion

            #region Fill Segments
            //Fill SegmentA
            if (IsNumberAvailable(number, 0, 2, 3, 5, 6, 7, 8, 9))
            {
                g.FillPolygon(fillPen.Brush, segmentA);
            }

            //Fill SegmentB
            if (IsNumberAvailable(number, 0, 1, 2, 3, 4, 7, 8, 9))
            {
                g.FillPolygon(fillPen.Brush, segmentB);
            }

            //Fill SegmentC
            if (IsNumberAvailable(number, 0, 1, 3, 4, 5, 6, 7, 8, 9))
            {
                g.FillPolygon(fillPen.Brush, segmentC);
            }

            //Fill SegmentD
            if (IsNumberAvailable(number, 0, 2, 3, 5, 6, 8, 9))
            {
                g.FillPolygon(fillPen.Brush, segmentD);
            }

            //Fill SegmentE
            if (IsNumberAvailable(number, 0, 2, 6, 8))
            {
                g.FillPolygon(fillPen.Brush, segmentE);
            }

            //Fill SegmentF
            if (IsNumberAvailable(number, 0, 4, 5, 6, 7, 8, 9))
            {
                g.FillPolygon(fillPen.Brush, segmentF);
            }

            //Fill SegmentG
            if (IsNumberAvailable(number, 2, 3, 4, 5, 6, 8, 9, -1))
            {
                g.FillPolygon(fillPen.Brush, segmentG);
            }
            #endregion

            //Draw decimal point
            if (dp)
            {
                g.FillEllipse(fillPen.Brush, new RectangleF(
                    position.X + GetX(10F, width),
                    position.Y + GetY(12F, height),
                    width / 7,
                    width / 7));
            }
        }

        /// <summary>
        /// Gets Relative X for the given width to draw digit
        /// </summary>
        /// <param name="x"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private float GetX(float x, float width)
        {
            return x * width / 12;
        }

        /// <summary>
        /// Gets relative Y for the given height to draw digit
        /// </summary>
        /// <param name="y"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private float GetY(float y, float height)
        {
            return y * height / 15;
        }

        /// <summary>
        /// Returns true if a given number is available in the given list.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="listOfNumbers"></param>
        /// <returns></returns>
        private bool IsNumberAvailable(int number, params int[] listOfNumbers)
        {
            if (listOfNumbers.Length > 0)
            {
                foreach (int i in listOfNumbers)
                {
                    if (i == number)
                        return true;
                }
            }
            return false;
        }
        #endregion
    }
}
