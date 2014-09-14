using QF = Quixo.Framework;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace Quixo.Controls
{
	public sealed class Piece : UserControl
	{
		public enum SelectedState
		{
			None, CanBeMoved, Selected
		}

		public EventHandler Selected;

		private System.ComponentModel.Container components = null;
		private SelectedState currentSelectedState = SelectedState.None;
		private QF.Piece piece = null;
		private QF.Player currentPlayer = QF.Player.None;

		private Piece() : base() { }

		internal Piece(QF.Piece piece)
			: this()
		{
			this.InitializeComponent();
			this.SetStyle(ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			this.piece = piece;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// Piece
			// 
			this.Name = "Piece";
			this.Size = new System.Drawing.Size(136, 136);

		}
		#endregion

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			base.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			this.DrawPiece(e.Graphics);
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);

			if (this.currentSelectedState != SelectedState.None)
			{
				if (this.Selected != null)
				{
					this.Selected(this, EventArgs.Empty);
				}
			}
		}

		private void DrawPiece(Graphics g)
		{
			Rectangle rectangle = this.ClientRectangle;
			g.FillRectangle(Brushes.Black, rectangle);

			switch (this.currentPlayer)
			{
				case QF.Player.X:
					this.DrawXPiece(g);
					break;
				case QF.Player.O:
					this.DrawOPiece(g);
					break;
				default:
					break;
			}

			switch (this.currentSelectedState)
			{
				case SelectedState.CanBeMoved:
					this.DrawBorder(g, Color.Yellow);
					break;
				case SelectedState.Selected:
					this.DrawBorder(g, Color.Red);
					break;
				default:
					break;
			}
		}

		private void DrawBorder(Graphics g, Color borderColor)
		{
			Rectangle rectangle = this.ClientRectangle;
			rectangle.Inflate(-1, -1);

			if (rectangle.Width > 0 && rectangle.Height > 0)
			{
				using (Brush valueBrush = new SolidBrush(borderColor))
				{
					using (Pen valuePen = new Pen(valueBrush, 2F))
					{
						g.DrawRectangle(valuePen, rectangle);
					}
				}
			}
		}

		private void DrawXPiece(Graphics g)
		{
			Rectangle rectangle = this.ClientRectangle;
			rectangle.Inflate(-4, -4);

			if (rectangle.Width > 0 && rectangle.Height > 0)
			{
				using (Brush valueBrush = new LinearGradientBrush(this.ClientRectangle, Color.Orange, Color.Red, 45F))
				{
					float penWidth = ((rectangle.Width + rectangle.Height) / 2F) / 14F;

					using (Pen valuePen = new Pen(valueBrush, penWidth))
					{
						g.DrawLine(valuePen, rectangle.X, rectangle.Y,
							 rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
						g.DrawLine(valuePen, rectangle.X, rectangle.Y + rectangle.Height,
							 rectangle.X + rectangle.Width, rectangle.Y);
					}
				}
			}
		}

		private void DrawOPiece(Graphics g)
		{
			Rectangle rectangle = this.ClientRectangle;
			rectangle.Inflate(-4, -4);

			if (rectangle.Width > 0 && rectangle.Height > 0)
			{
				using (Brush valueBrush = new LinearGradientBrush(this.ClientRectangle, Color.Orange, Color.Red, 45F))
				{
					float penWidth = ((rectangle.Width + rectangle.Height) / 2F) / 14F;
					using (Pen valuePen = new Pen(valueBrush, penWidth))
					{
						g.DrawEllipse(valuePen, rectangle);
					}
				}
			}
		}

		internal QF.Piece RepresentedPiece
		{
			get
			{
				return this.piece;
			}
		}

		internal SelectedState CurrentSelectedState
		{
			get
			{
				return this.currentSelectedState;
			}
			set
			{
				this.currentSelectedState = value;
				this.Invalidate();
			}
		}

		internal QF.Player CurrentPlayer
		{
			get
			{
				return this.currentPlayer;
			}
			set
			{
				this.currentPlayer = value;
				this.Invalidate();
			}
		}
	}
}
