namespace CodeMind.BookmarksConverter
{
    /// <summary>
    /// Encapsulates command line arguments.
    /// </summary>
    public class ApplicationArguments
    {
        /// <summary>
        /// Gets or sets the input format type.
        /// </summary>
        public FormatType Type { get; set; }

        /// <summary>
        /// Gets or sets the input file path.
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Gets or sets the output file path.
        /// </summary>
        public string Output { get; set; }
    }
}