using Alternet.Drawing;
using Alternet.UI;

namespace DrawingSample
{
    public class CanvasControl : Control
    {
        public CanvasControl()
        {
            UserPaint = true;
            Background = Brushes.White;
        }
    }
}