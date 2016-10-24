namespace Pipes
{
    /// <summary>
    /// Enum of possible errors from C
    /// </summary>
    public enum PipesError
    {
        /// <summary>
        /// Permission Denied
        /// </summary>
        EACCES = 13,
        /// <summary>
        /// Quota exceeded
        /// </summary>
        EDQUOT = 122,
        /// <summary>
        /// File exists
        /// </summary>
        EEXIST = 17,
        /// <summary>
        /// File name too long
        /// </summary>
        ENAMETOOLONG = 36,
        /// <summary>
        /// No such file or directory
        /// </summary>
        ENOENT = 2,
        /// <summary>
        /// No space left on device
        /// </summary>
        ENOSPC = 28,
        /// <summary>
        /// Not a directory
        /// </summary>
        ENOTDIR = 20,
        /// <summary>
        /// Read-only file system
        /// </summary>
        EROFS = 30,
        //unlink
        /// <summary>
        /// Device or resource busy
        /// </summary>
        EBUSY = 16,
        /// <summary>
        /// Bad address
        /// </summary>
        EFAULT = 14,
        /// <summary>
        /// I/O Error
        /// </summary>
        EIO = 5,
        /// <summary>
        /// Too many symbolic links encountered
        /// </summary>
        ELOOP = 40,
        /// <summary>
        /// Out of memory
        /// </summary>
        ENOMEM = 12,
        /// <summary>
        /// Operation not permitted
        /// </summary>
        EPERM = 1,
    }
}