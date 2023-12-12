// A class to pass as an argument for the event handlers (EventArgs class)
public class AttendanceEventArgs : EventArgs
{
    public bool IsPresent { get; }

    public AttendanceEventArgs(bool isPresent)
    {
        IsPresent = isPresent;
    }
}

// The delegate for the event
public delegate void AttendanceChangedEventHandler(object sender, AttendanceEventArgs e);

public class StudentAttendance
{
    public event AttendanceChangedEventHandler AttendanceChanged;

    private bool isPresent;

    public bool IsPresent
    {
        get { return isPresent; }
        set
        {
            if (isPresent != value)
            {
                isPresent = value;

                // Associate the event with the event handler
                OnAttendanceChanged(new AttendanceEventArgs(value));
            }
        }
    }

    // Code that will be run when the event occurs (Event Handler)
    protected virtual void OnAttendanceChanged(AttendanceEventArgs e)
    {
        AttendanceChanged?.Invoke(this, e);
    }
}

public class AttendanceTracker
{
    // Event handler method
    public void OnAttendanceChanged(object sender, AttendanceEventArgs e)
    {
        string status = e.IsPresent ? "present" : "absent";
        Console.WriteLine($"Attendance changed. Student is now {status}");
    }
}

class Program
{
    static void Main()
    {
        StudentAttendance studentAttendance = new StudentAttendance();
        AttendanceTracker attendanceTracker = new AttendanceTracker();

        //Associate the event with the event handler
        studentAttendance.AttendanceChanged += attendanceTracker.OnAttendanceChanged;

        studentAttendance.IsPresent = true;
        studentAttendance.IsPresent = false;
        studentAttendance.IsPresent = true;
    }
}
