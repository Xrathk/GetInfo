namespace DataAccessLayer.Models.StatusEnums
{
    /// <summary>
    /// Web form submission statuses.
    /// </summary>
    public enum FormSubmissionStatus
    {
        NotSent = 0,
        CurrentlyProcessed = 1,
        Rejected = 2,
        Accepted = 3
    }
}
