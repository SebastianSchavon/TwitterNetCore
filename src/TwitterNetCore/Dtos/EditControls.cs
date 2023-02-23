namespace TwitterNetCore.Dtos;

public class EditControls
{
    public int edits_remaining { get; set; }
    public bool is_edit_eligible { get; set; }
    public DateTime editable_until { get; set; }
}