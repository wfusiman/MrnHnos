using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MoreniApp.Helpers
{
    public class FocusControl
    {
        public static void checkMoveNext(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                // Creating a FocusNavigationDirection object and setting it to a
                // local field that contains the direction selected.
                FocusNavigationDirection focusDirection = FocusNavigationDirection.Next;

                // MoveFocus takes a TraveralReqest as its argument.
                TraversalRequest request = new TraversalRequest(focusDirection);

                // Gets the element with keyboard focus.
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

                // Change keyboard focus.
                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(request);
                }
            }
        }
    }
}
