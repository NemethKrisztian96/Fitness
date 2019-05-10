using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fitness.Common.Helpers
{
    public static class CustomDateTime
    {
        public static DateTimePicker GetDateTimePicker()
        {
            // Create a new DateTimePicker control and initialize it.
            DateTimePicker dateTimePicker = new DateTimePicker();

            // Set the MinDate and MaxDate.
            dateTimePicker.MinDate = new DateTime(1900, 1, 1);
            dateTimePicker.MaxDate = new DateTime(2025, 12, 31);

            // Set the CustomFormat string.
            dateTimePicker.CustomFormat = "dd MMM yyyy";
            dateTimePicker.Format = DateTimePickerFormat.Custom;

            // Show the CheckBox and display the control as an up-down control.
            dateTimePicker.ShowCheckBox = true;
            dateTimePicker.ShowUpDown = true;

            return dateTimePicker;
        }

        public static DateTimePicker GetDateTimePicker(DateTime minDate, DateTime maxDate)
        {
            // Create a new DateTimePicker control and initialize it.
            DateTimePicker dateTimePicker = new DateTimePicker();

            // Set the MinDate and MaxDate.
            dateTimePicker.MinDate = minDate;
            dateTimePicker.MaxDate = maxDate;

            // Set the CustomFormat string.
            dateTimePicker.CustomFormat = "dd MMM yyyy";
            dateTimePicker.Format = DateTimePickerFormat.Custom;

            // Show the CheckBox and display the control as an up-down control.
            dateTimePicker.ShowCheckBox = true;
            dateTimePicker.ShowUpDown = true;

            return dateTimePicker;
        }

        public static DateTime GetToday()
        {
            return DateTime.Today;
        }

        public static string GetFormatedString(DateTime date)
        {
            return date.ToString("dd MMM yyyy");
        }
    }
}
