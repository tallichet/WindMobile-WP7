using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Ch.Epyx.WindMobile.WP7.Model
{
    /// <summary>
    /// Used to send a message
    /// </summary>
    public interface ISendMessage
    {
        /// <summary>
        /// Credentials to send the message
        /// </summary>
        ICredentials Credentials { get; }

        /// <summary>
        /// ID of the chart room (station) the message is associated with
        /// </summary>
        string ChatRoomId { get; }

        /// <summary>
        /// The message itself
        /// </summary>
        string Message { get; }

        string BasicAuthentication { get; }
    }
}
