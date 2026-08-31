using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace OpenCVForUnity.UnityIntegration
{
    /// <summary>
    /// OpenCV Environment utilities.
    /// </summary>
    public static class OpenCVEnv
    {
        /// <summary>
        /// Returns this "OpenCV for Unity" version number.
        /// </summary>
        /// <returns>
        /// this "OpenCV for Unity" version number
        /// </returns>
        public static string GetVersion()
        {
            return "3.0.1";
        }


        #region GetFilePath

        /// <summary>
        /// Copies a file from the "StreamingAssets" directory to a readable location.
        /// </summary>
        /// <remarks>
        /// Provide a relative file path based on the "StreamingAssets" directory. e.g., "foobar.txt" or "hogehoge/foobar.txt".
        /// [Android] The target file that exists in the "StreamingAssets" directory is copied to the Application.persistentDataPath directory.
        /// [WebGL] If the target file has not yet been copied to WebGL's virtual filesystem, it is necessary to use <see cref="CopyFileCoroutine">CopyFileCoroutine</see>, <see cref="CopyFileAsync">CopyFileAsync</see> or <see cref="CopyFileTaskAsync">CopyFileAsyncTask</see> at first.
        /// </remarks>
        /// <param name="filepath">
        /// A file path relative to the "StreamingAssets" directory.
        /// </param>
        /// <param name="refresh">
        /// [Android] If false, the file is not copied if it already exists. If true, the file is always copied.
        /// </param>
        /// <param name="timeout">
        /// [Android] Sets the UnityWebRequest to abort after the specified number of seconds. If set to 0, no timeout is applied. The default is 0.
        /// </param>
        /// <returns>
        /// Returns the copied file path in case of success and returns <c>string.Empty</c> in case of error.
        /// </returns>
        private static string CopyFile(string filepath, bool refresh, int timeout)
        {
            string srcPath = Path.Combine(Application.streamingAssetsPath, filepath);

#if UNITY_ANDROID
            string destPath = Path.Combine(Application.persistentDataPath, "opencvforunity", filepath);
#elif UNITY_WEBGL
            string destPath = Path.Combine(Path.DirectorySeparatorChar.ToString(), "opencvforunity", filepath);
#else
            string destPath = Path.Combine(Application.streamingAssetsPath, filepath);
#endif

            if (!refresh && File.Exists(destPath))
                return destPath;

            using (UnityWebRequest request = UnityWebRequest.Get(srcPath))
            {
                if (timeout > 0)
                    request.timeout = timeout;

                request.SendWebRequest();
                while (!request.isDone) { }

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(destPath));
                    File.WriteAllBytes(destPath, request.downloadHandler.data);
                    return destPath;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the readable path of a file in the "StreamingAssets" directory.
        /// </summary>
        /// <remarks>
        /// Provide a relative file path based on the "StreamingAssets" directory. e.g., "foobar.txt" or "hogehoge/foobar.txt".
        /// [Android] The target file that exists in the "StreamingAssets" directory is copied to the the Application.persistentDataPath directory.
        /// [WebGL] If the target file has not yet been copied to WebGL's virtual filesystem, it is necessary to use <see cref="GetFilePathCoroutine">GetFilePathCoroutine</see>, <see cref="GetFilePathAsync">GetFilePathAsync</see> or <see cref="GetFilePathTaskAsync">GetFilePathAsyncTask</see> at first.
        /// </remarks>
        /// <param name="filepath">
        /// A file path relative to the "StreamingAssets" directory.
        /// </param>
        /// <param name="refresh">
        /// [Android] If false, the file is not copied if it already exists. If true, the file is always copied.
        /// </param>
        /// <param name="timeout">
        /// [Android] Sets the UnityWebRequest to abort after the specified number of seconds. If set to 0, no timeout is applied. The default is 0.
        /// </param>
        /// <returns>
        /// Returns the readable file path in case of success and returns <c>string.Empty</c> in case of error.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="filepath"/> is null.
        /// </exception>
        public static string GetFilePath(string filepath, bool refresh = false, int timeout = 0)
        {
            if (filepath == null)
                throw new ArgumentNullException(nameof(filepath));

            filepath = filepath.TrimStart(chTrims);

            if (string.IsNullOrEmpty(filepath) || string.IsNullOrEmpty(Path.GetExtension(filepath)))
                return string.Empty;

#if (UNITY_ANDROID || UNITY_WEBGL) && !UNITY_EDITOR
#if UNITY_ANDROID
            string destPath = CopyFile(filepath, refresh, timeout);
            return destPath;
#else // UNITY_WEBGL
            string destPath = Path.Combine(Path.DirectorySeparatorChar.ToString(), "opencvforunity", filepath);
            if (File.Exists(destPath)) {
                return destPath;
            }
            else
            {
                Debug.LogWarning($"The file does not exist: {filepath}. If the target file has not yet been copied to WebGL's virtual filesystem, it is necessary to use GetFilePathCoroutine, GetFilePathAsync or GetFilePathAsyncTask at first.");
                return string.Empty;
            }
#endif
#else // (UNITY_ANDROID || UNITY_WEBGL) && !UNITY_EDITOR
            string destPath = Path.Combine(Application.streamingAssetsPath, filepath);
            return File.Exists(destPath) ? destPath : string.Empty;
#endif // (UNITY_ANDROID || UNITY_WEBGL) && !UNITY_EDITOR
        }

        /// <summary>
        /// Gets the multiple readable paths of a file in the "StreamingAssets" directory.
        /// </summary>
        /// <remarks>
        /// Provide a relative file path based on the "StreamingAssets" directory.  e.g., "foobar.txt" or "hogehoge/foobar.txt".
        /// [Android] The target file that exists in the "StreamingAssets" directory is copied to the the Application.persistentDataPath directory.
        /// [WebGL] If the target file has not yet been copied to WebGL's virtual filesystem, it is necessary to use <see cref="GetFilePathCoroutine">GetFilePathCoroutine</see>, <see cref="GetFilePathAsync">GetFilePathAsync</see> or <see cref="GetFilePathTaskAsync">GetFilePathAsyncTask</see> at first.
        /// </remarks>
        /// <param name="filepaths">
        /// The list of file paths relative to the "StreamingAssets" directory.
        /// </param>
        /// <param name="refresh">
        /// [Android] If false, the file is not copied if it already exists. If true, the file is always copied.
        /// </param>
        /// <param name="timeout">
        /// [Android] Sets the UnityWebRequest to abort after the specified number of seconds. If set to 0, no timeout is applied. The default is 0.
        /// </param>
        /// <returns>
        /// Returns the list of readable file paths in case of success and returns <c>string.Empty</c> in case of error.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="filepath"/> is null.
        /// </exception>
        public static IReadOnlyList<string> GetMultipleFilePaths(IReadOnlyList<string> filepaths, bool refresh = false, int timeout = 0)
        {
            if (filepaths == null)
                throw new ArgumentNullException(nameof(filepaths));

            var results = new string[filepaths.Count];

            for (int i = 0; i < filepaths.Count; i++)
            {
                results[i] = GetFilePath(filepaths[i], refresh, timeout);
            }

            return results;
        }

        #endregion

        #region GetFilePathCoroutine

        /// <summary>
        /// Asynchronously copies a file from the "StreamingAssets" directory to a readable location using Coroutine.
        /// </summary>
        /// <remarks>
        /// Provide a relative file path based on the "StreamingAssets" directory. e.g., "foobar.txt" or "hogehoge/foobar.txt".
        /// [Android] The target file that exists in the "StreamingAssets" directory is copied to the Application.persistentDataPath directory.
        /// [WebGL] If the target file has not yet been copied to WebGL's virtual filesystem, it is necessary to use this method before accessing the file.
        /// </remarks>
        /// <param name="filepath">
        /// A file path relative to the "StreamingAssets" directory.
        /// </param>
        /// <param name="completed">
        /// A callback that is called when the process is completed. Returns the copied file path in case of success and returns <c>string.Empty</c> in case of error.
        /// </param>
        /// <param name="progressChanged">
        /// An optional callback that is called when the process is in progress. Returns the file path and a progress value (0.0 to 1.0).
        /// </param>
        /// <param name="errorOccurred">
        /// An optional callback that is called when an error occurs. Returns the file path, an error string, and an error response code.
        /// </param>
        /// <param name="refresh">
        /// [Android] If false, the file is not copied if it already exists. If true, the file is always copied.
        /// </param>
        /// <param name="timeout">
        /// [Android] Sets the UnityWebRequest to abort after the specified number of seconds. If set to 0, no timeout is applied. The default is 0.
        /// </param>
        /// <returns>
        /// Returns an IEnumerator object. Yielding the IEnumerator in a coroutine pauses execution until the UnityWebRequest completes or encounters an error.
        /// </returns>
        private static IEnumerator CopyFileCoroutine(string filepath, Action<string> completed, Action<string, float> progressChanged = null, Action<string, string, long> errorOccurred = null, bool refresh = false, int timeout = 0)
        {
            string srcPath = Path.Combine(Application.streamingAssetsPath, filepath);

#if UNITY_ANDROID
            string destPath = Path.Combine(Application.persistentDataPath, "opencvforunity", filepath);
#elif UNITY_WEBGL
            string destPath = Path.Combine(Path.DirectorySeparatorChar.ToString(), "opencvforunity", filepath);
#else
            string destPath = Path.Combine(Application.streamingAssetsPath, filepath);
#endif

            if (!refresh && File.Exists(destPath))
            {
                progressChanged?.Invoke(filepath, 0);
                yield return null;
                progressChanged?.Invoke(filepath, 1);
                completed?.Invoke(destPath);
                yield break;
            }


            using (UnityWebRequest request = UnityWebRequest.Get(srcPath))
            {
                if (timeout > 0)
                    request.timeout = timeout;

                var operation = request.SendWebRequest();
                while (!operation.isDone)
                {
                    progressChanged?.Invoke(filepath, request.downloadProgress);
                    yield return null;
                }

                if (request.result == UnityWebRequest.Result.Success)
                {
                    progressChanged?.Invoke(filepath, request.downloadProgress);
                    // create directory and write downlorded data
                    string dirPath = Path.GetDirectoryName(destPath);
                    if (!Directory.Exists(dirPath))
                        Directory.CreateDirectory(dirPath);
                    File.WriteAllBytes(destPath, request.downloadHandler.data);
                    completed?.Invoke(destPath);
                    yield break;
                }
                else
                {
                    //Debug.LogError($"UnityWebRequest error occurred: {filepath}, {request.error}, {request.responseCode}");
                    errorOccurred?.Invoke(filepath, request.error, request.responseCode);
                    completed?.Invoke(string.Empty);
                    yield break;
                }
            }
        }

        /// <summary>
        /// Asynchronously retrieves the readable path of a file in the "StreamingAssets" directory using Coroutine.
        /// </summary>
        /// <remarks>
        /// Provide a relative file path based on the "StreamingAssets" directory.  e.g., "foobar.txt" or "hogehoge/foobar.txt".
        /// [Android] The target file that exists in the "StreamingAssets" directory is copied to the the Application.persistentDataPath directory.
        /// [WebGL] The target file in the "StreamingAssets" directory is copied to the WebGL's virtual filesystem.
        /// </remarks>
        /// <param name="filepath">
        /// A file path relative to the "StreamingAssets" directory.
        /// </param>
        /// <param name="completed">
        /// A callback that is called when the process is completed. Returns a readable file path in case of success and returns <c>string.Empty</c> in case of error.
        /// </param>
        /// <param name="progressChanged">
        /// An optional callback that is called when the process is the progress. Returns the file path and a progress value (0.0 to 1.0).
        /// </param>
        /// <param name="errorOccurred">
        /// An optional callback that is called when the process is error occurred. Returns the file path and an error string and an error response code.
        /// </param>
        /// <param name="refresh">
        /// [Android][WebGL] If false, the file is not copied if it already exists. If true, the file is always copied.
        /// </param>
        /// <param name="timeout">
        /// [Android][WebGL] Sets the UnityWebRequest to abort after the specified number of seconds. If set to 0, no timeout is applied. The default is 0.
        /// </param>
        /// <returns>
        /// Returns an IEnumerator object. Yielding the IEnumerator in a coroutine pauses the coroutine until the UnityWebRequest completes or encounters a system error.
        /// <strong>Note:</strong> that if the IEnumerator is externally stoped, the UnityWebRequest's Abort method will not be called, meaning the download will continue in the background.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="filepath"/> is null.
        /// </exception>
        public static IEnumerator GetFilePathCoroutine(
            string filepath,
            Action<string> completed,
            Action<string, float> progressChanged = null,
            Action<string, string, long> errorOccurred = null,
            bool refresh = false,
            int timeout = 0)
        {
            if (filepath == null)
                throw new ArgumentNullException(nameof(filepath));

            filepath = filepath.TrimStart(chTrims);

            if (string.IsNullOrEmpty(filepath) || string.IsNullOrEmpty(Path.GetExtension(filepath)))
            {
                progressChanged?.Invoke(filepath, 0);
                yield return null;
                progressChanged?.Invoke(filepath, 1);
                errorOccurred?.Invoke(filepath, "Invalid file path.", -1);
                completed?.Invoke(string.Empty);

                yield break;
            }

#if (UNITY_ANDROID || UNITY_WEBGL) && !UNITY_EDITOR
            yield return CopyFileCoroutine(filepath, completed, progressChanged, errorOccurred, refresh, timeout);
#else // (UNITY_ANDROID || UNITY_WEBGL) && !UNITY_EDITOR
            string destPath = Path.Combine(Application.streamingAssetsPath, filepath);

            progressChanged?.Invoke(filepath, 0);
            yield return null;
            progressChanged?.Invoke(filepath, 1);

            if (File.Exists(destPath))
            {
                completed?.Invoke(destPath);
            }
            else
            {
                errorOccurred?.Invoke(filepath, "File does not exist.", -1);
                completed?.Invoke(string.Empty);
            }
            yield break;
#endif // (UNITY_ANDROID || UNITY_WEBGL) && !UNITY_EDITOR

        }

        /// <summary>
        /// Asynchronously retrieves the multiple readable paths of files in the "StreamingAssets" directory using Coroutine.
        /// </summary>
        /// <remarks>
        /// Provide a relative file path based on the "StreamingAssets" directory.  e.g., "foobar.txt" or "hogehoge/foobar.txt".
        /// [Android] The target file that exists in the "StreamingAssets" directory is copied to the the Application.persistentDataPath directory.
        /// [WebGL] The target file in the "StreamingAssets" directory is copied to the WebGL's virtual filesystem.
        /// </remarks>
        /// <param name="filepaths">
        /// The list of file paths relative to the "StreamingAssets" directory.
        /// </param>
        /// <param name="allCompleted">
        /// A callback that is called when all processes are completed. Returns a list of file paths. Returns a readable file path in case of success and returns <c>string.Empty</c> in case of error.
        /// </param>
        /// <param name="completed">
        /// An optional callback that is called when one process is completed. Returns a readable file path in case of success and returns empty in case of error.
        /// </param>
        /// <param name="progressChanged">
        /// An optional callback that is called when one process is the progress. Returns the file path and a progress value (0.0 to 1.0).
        /// </param>
        /// <param name="errorOccurred">
        /// An optional callback that is called when one process is error occurred. Returns the file path and an error string and an error response code.
        /// </param>
        /// <param name="refresh">
        /// [Android][WebGL] If false, the file is not copied if it already exists. If true, the file is always copied.
        /// </param>
        /// <param name="timeout">
        /// [Android][WebGL] Sets the UnityWebRequest to abort after the specified number of seconds. If set to 0, no timeout is applied. The default is 0.
        /// </param>
        /// <returns>
        /// Returns an IEnumerator object. Yielding the IEnumerator in a coroutine pauses the coroutine until the UnityWebRequest completes or encounters a system error.
        /// <strong>Note:</strong> that if the IEnumerator is externally stoped, the UnityWebRequest's Abort method will not be called, meaning the download will continue in the background.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="filepath"/> is null.
        /// </exception>
        public static IEnumerator GetMultipleFilePathsCoroutine(
            IReadOnlyList<string> filepaths,
            Action<IReadOnlyList<string>> allCompleted,
            Action<string> completed = null,
            Action<string, float> progressChanged = null,
            Action<string, string, long> errorOccurred = null,
            bool refresh = false,
            int timeout = 0)
        {
            if (filepaths == null)
                throw new ArgumentNullException(nameof(filepaths));

            string[] readableFilePaths = new string[filepaths.Count];

            for (int i = 0; i < filepaths.Count; i++)
            {
                yield return GetFilePathCoroutine(filepaths[i],
                (path) =>
                {
                    readableFilePaths[i] = path;
                    completed?.Invoke(path);
                },
                progressChanged,
                errorOccurred,
                refresh, timeout);
            }

            allCompleted?.Invoke(readableFilePaths);
        }

        #endregion

        #region GetFilePathAsync

#if UNITY_2023_1_OR_NEWER

        /// <summary>
        /// Asynchronously copies a file from the "StreamingAssets" directory to a readable location using Awaitable.
        /// </summary>
        /// <remarks>
        /// Provide a relative file path based on the "StreamingAssets" directory. e.g., "foobar.txt" or "hogehoge/foobar.txt".
        /// [Android] The target file that exists in the "StreamingAssets" directory is copied to the Application.persistentDataPath directory.
        /// [WebGL] If the target file has not yet been copied to WebGL's virtual filesystem, it is necessary to use this method before accessing the file.
        /// </remarks>
        /// <param name="filepath">
        /// A file path relative to the "StreamingAssets" directory.
        /// </param>
        /// <param name="progressChanged">
        /// An optional callback that is called when the process is in progress. Returns the file path and a progress value (0.0 to 1.0).
        /// </param>
        /// <param name="errorOccurred">
        /// An optional callback that is called when an error occurs. Returns the file path, an error string, and an error response code.
        /// </param>
        /// <param name="refresh">
        /// [Android] If false, the file is not copied if it already exists. If true, the file is always copied.
        /// </param>
        /// <param name="timeout">
        /// [Android] Sets the UnityWebRequest to abort after the specified number of seconds. If set to 0, no timeout is applied. The default is 0.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the download operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous copy operation. The result is the copied file path if successful, or <c>string.Empty</c> if the operation fails.
        /// </returns>
        /// <exception cref="OperationCanceledException">
        /// Thrown if the copy operation is canceled.
        /// </exception>
        private static async Awaitable<string> CopyFileAsync(string filepath, Action<string, float> progressChanged, Action<string, string, long> errorOccurred, bool refresh, int timeout, CancellationToken cancellationToken)
        {
            string srcPath = Path.Combine(Application.streamingAssetsPath, filepath);

#if UNITY_ANDROID
            string destPath = Path.Combine(Application.persistentDataPath, "opencvforunity", filepath);
#elif UNITY_WEBGL
            string destPath = Path.Combine(Path.DirectorySeparatorChar.ToString(), "opencvforunity", filepath);
#else
            string destPath = Path.Combine(Application.streamingAssetsPath, filepath);
#endif

            if (!refresh && File.Exists(destPath))
            {
                progressChanged?.Invoke(filepath, 0);
                await Awaitable.NextFrameAsync();
                progressChanged?.Invoke(filepath, 1);
                return destPath;
            }


            using (UnityWebRequest request = UnityWebRequest.Get(srcPath))
            {
                if (timeout > 0)
                    request.timeout = timeout;

                var operation = request.SendWebRequest();

                try
                {

                    while (!operation.isDone)
                    {
                        progressChanged?.Invoke(filepath, request.downloadProgress);
                        await Awaitable.NextFrameAsync();

                        if (cancellationToken.IsCancellationRequested)
                        {
                            request.Abort();
                            cancellationToken.ThrowIfCancellationRequested();
                        }
                    }

                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(destPath));
                        File.WriteAllBytes(destPath, request.downloadHandler.data);
                        return destPath;
                    }
                    else
                    {
                        errorOccurred?.Invoke(filepath, request.error, request.responseCode);
                        return string.Empty;
                    }

                }
                catch (OperationCanceledException)
                {
                    //Debug.Log($"Download canceled: {filepath}");
                    throw;
                }
                catch (Exception ex)
                {
                    //Debug.LogError($"An error occurred: {filepath}, {ex.Message}");
                    errorOccurred?.Invoke(filepath, ex.Message, -1);
                    request.Abort();
                    return string.Empty;
                }
            }
        }

        private static async Task<T> AsTask<T>(this Awaitable<T> awaitable)
        {
            return await awaitable;
        }

        /// <summary>
        /// Asynchronously retrieves the readable path of a file in the "StreamingAssets" directory using Awaitable.
        /// </summary>
        /// <remarks>
        /// Provide a relative file path based on the "StreamingAssets" directory.  e.g., "foobar.txt" or "hogehoge/foobar.txt".
        /// [Android] The target file that exists in the "StreamingAssets" directory is copied to the the Application.persistentDataPath directory.
        /// [WebGL] The target file in the "StreamingAssets" directory is copied to the WebGL's virtual filesystem.
        /// </remarks>
        /// <param name="filepath">
        /// A file path relative to the "StreamingAssets" directory.
        /// </param>
        /// <param name="progressChanged">
        /// An optional callback that is called when the process is the progress. Returns the file path and a progress value (0.0 to 1.0).
        /// </param>
        /// <param name="errorOccurred">
        /// An optional callback that is called when the process is error occurred. Returns the file path and an error string and an error response code.
        /// </param>
        /// <param name="refresh">
        /// [Android][WebGL] If false, the file is not copied if it already exists. If true, the file is always copied.
        /// </param>
        /// <param name="timeout">
        /// [Android][WebGL] Sets the UnityWebRequest to abort after the specified number of seconds. If set to 0, no timeout is applied. The default is 0.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the download operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous download operation. The result is a readable file path where the downloaded file was saved, or <c>string.Empty</c> if the download fails.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="filepath"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this method is called from a non-main thread.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown if the download operation is canceled.
        /// </exception>
        public static async Awaitable<string> GetFilePathAsync(
            string filepath,
            Action<string, float> progressChanged = null,
            Action<string, string, long> errorOccurred = null,
            bool refresh = false,
            int timeout = 0,
            CancellationToken cancellationToken = default)
        {
            if (filepath == null)
                throw new ArgumentNullException(nameof(filepath));

            if (SynchronizationContext.Current == null)
                throw new InvalidOperationException("This method must be called from the main thread.");

            cancellationToken.ThrowIfCancellationRequested();

            filepath = filepath.TrimStart(chTrims);

            if (string.IsNullOrEmpty(filepath) || string.IsNullOrEmpty(Path.GetExtension(filepath)))
            {
                progressChanged?.Invoke(filepath, 0);
                await Awaitable.NextFrameAsync();

                cancellationToken.ThrowIfCancellationRequested();

                progressChanged?.Invoke(filepath, 1);
                errorOccurred?.Invoke(filepath, "Invalid file path.", -1);

                return string.Empty;
            }

#if (UNITY_ANDROID || UNITY_WEBGL) && !UNITY_EDITOR
            string destPath = await CopyFileAsync(filepath, progressChanged, errorOccurred, refresh, timeout, cancellationToken);
            return destPath;
#else // (UNITY_ANDROID || UNITY_WEBGL) && !UNITY_EDITOR
            string destPath = Path.Combine(Application.streamingAssetsPath, filepath);

            progressChanged?.Invoke(filepath, 0);
            await Awaitable.NextFrameAsync();

            cancellationToken.ThrowIfCancellationRequested();

            progressChanged?.Invoke(filepath, 1);

            if (File.Exists(destPath))
            {
                return destPath;
            }
            else
            {
                errorOccurred?.Invoke(filepath, "File does not exist.", -1);
                return string.Empty;
            }
#endif // (UNITY_ANDROID || UNITY_WEBGL) && !UNITY_EDITOR
        }

        /// <summary>
        /// Asynchronously retrieves the multiple readable paths of files in the "StreamingAssets" directory using Awaitable.
        /// </summary>
        /// <remarks>
        /// Provide a relative file path based on the "StreamingAssets" directory.  e.g., "foobar.txt" or "hogehoge/foobar.txt".
        /// [Android] The target file that exists in the "StreamingAssets" directory is copied to the the Application.persistentDataPath directory.
        /// [WebGL] The target file in the "StreamingAssets" directory is copied to the WebGL's virtual filesystem.
        /// </remarks>
        /// <param name="filepaths">
        /// The list of file paths relative to the "StreamingAssets" directory.
        /// </param>
        /// <param name="completed">
        /// An optional callback that is called when one process is completed. Returns a readable file path in case of success and returns empty in case of error.
        /// </param>
        /// <param name="progressChanged">
        /// An optional callback that is called when one process is the progress. Returns the file path and a progress value (0.0 to 1.0).
        /// </param>
        /// <param name="errorOccurred">
        /// An optional callback that is called when one process is error occurred. Returns the file path and an error string and an error response code.
        /// </param>
        /// <param name="refresh">
        /// [Android][WebGL] If false, the file is not copied if it already exists. If true, the file is always copied.
        /// </param>
        /// <param name="timeout">
        /// [Android][WebGL] Sets the UnityWebRequest to abort after the specified number of seconds. If set to 0, no timeout is applied. The default is 0.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the download operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous download operation. The result is a list of readable file paths where the downloaded file was saved, or <c>string.Empty</c> if the download fails.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="filepath"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this method is called from a non-main thread.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown if the download operation is canceled.
        /// </exception>
        public static async Awaitable<IReadOnlyList<string>> GetMultipleFilePathsAsync(
            IReadOnlyList<string> filepaths,
            Action<string> completed = null,
            Action<string, float> progressChanged = null,
            Action<string, string, long> errorOccurred = null,
            bool refresh = false,
            int timeout = 0,
            CancellationToken cancellationToken = default)
        {
            if (filepaths == null)
                throw new ArgumentNullException(nameof(filepaths));

            if (SynchronizationContext.Current == null)
                throw new InvalidOperationException("This method must be called from the main thread.");

            var downloadTasks = new List<Task<string>>(filepaths.Count);
            var results = new string[filepaths.Count];

            for (int i = 0; i < filepaths.Count; i++)
            {
                int index = i;

                var task = GetFilePathAsync(filepaths[index], progressChanged, errorOccurred, refresh, timeout, cancellationToken).AsTask().ContinueWith(t =>
                {
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        var result = t.Result;
                        completed?.Invoke(result);
                        return result;
                    }
                    return string.Empty;

                }, cancellationToken, TaskContinuationOptions.AttachedToParent, TaskScheduler.FromCurrentSynchronizationContext());


                downloadTasks.Add(task);
            }

            results = await Task.WhenAll(downloadTasks);

            return results;
        }

#endif

        #endregion

        #region GetFilePathTaskAsync

        /// <summary>
        /// Asynchronously copies a file from the "StreamingAssets" directory to a readable location using Task.
        /// </summary>
        /// <remarks>
        /// Provide a relative file path based on the "StreamingAssets" directory. e.g., "foobar.txt" or "hogehoge/foobar.txt".
        /// [Android] The target file that exists in the "StreamingAssets" directory is copied to the Application.persistentDataPath directory.
        /// [WebGL] If the target file has not yet been copied to WebGL's virtual filesystem, it is necessary to use this method before accessing the file.
        /// </remarks>
        /// <param name="filepath">
        /// A file path relative to the "StreamingAssets" directory.
        /// </param>
        /// <param name="progressChanged">
        /// An optional callback that is called when the process is in progress. Returns the file path and a progress value (0.0 to 1.0).
        /// </param>
        /// <param name="errorOccurred">
        /// An optional callback that is called when an error occurs. Returns the file path, an error string, and an error response code.
        /// </param>
        /// <param name="refresh">
        /// [Android] If false, the file is not copied if it already exists. If true, the file is always copied.
        /// </param>
        /// <param name="timeout">
        /// [Android] Sets the UnityWebRequest to abort after the specified number of seconds. If set to 0, no timeout is applied. The default is 0.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the copy operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous copy operation. The result is the copied file path if successful, or <c>string.Empty</c> if the operation fails.
        /// </returns>
        /// <exception cref="OperationCanceledException">
        /// Thrown if the copy operation is canceled.
        /// </exception>
        private static async Task<string> CopyFileTaskAsync(string filepath, Action<string, float> progressChanged, Action<string, string, long> errorOccurred, bool refresh, int timeout, CancellationToken cancellationToken)
        {

            string srcPath = Path.Combine(Application.streamingAssetsPath, filepath);

#if UNITY_ANDROID
            string destPath = Path.Combine(Application.persistentDataPath, "opencvforunity", filepath);
#elif UNITY_WEBGL
            string destPath = Path.Combine(Path.DirectorySeparatorChar.ToString(), "opencvforunity", filepath);
#else
            string destPath = Path.Combine(Application.streamingAssetsPath, filepath);
#endif

            if (!refresh && File.Exists(destPath))
            {
                progressChanged?.Invoke(filepath, 0);
                await Task.Yield();
                progressChanged?.Invoke(filepath, 1);
                return destPath;
            }


            using (UnityWebRequest request = UnityWebRequest.Get(srcPath))
            {
                if (timeout > 0)
                    request.timeout = timeout;

                var operation = request.SendWebRequest();

                try
                {

                    while (!operation.isDone)
                    {
                        progressChanged?.Invoke(filepath, request.downloadProgress);
                        await Task.Yield();

                        if (cancellationToken.IsCancellationRequested)
                        {
                            request.Abort();
                            cancellationToken.ThrowIfCancellationRequested();
                        }
                    }

                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(destPath));
                        File.WriteAllBytes(destPath, request.downloadHandler.data);
                        return destPath;
                    }
                    else
                    {
                        errorOccurred?.Invoke(filepath, request.error, request.responseCode);
                        request.Abort();
                        return string.Empty;
                    }

                }
                catch (OperationCanceledException)
                {
                    //Debug.Log($"Download canceled: {filepath}");
                    throw;
                }
                catch (Exception ex)
                {
                    //Debug.LogError($"An error occurred: {filepath}, {ex.Message}");
                    errorOccurred?.Invoke(filepath, ex.Message, -1);
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Asynchronously retrieves the readable path of a file in the "StreamingAssets" directory using Task.
        /// </summary>
        /// <remarks>
        /// Provide a relative file path based on the "StreamingAssets" directory.  e.g., "foobar.txt" or "hogehoge/foobar.txt".
        /// [Android] The target file that exists in the "StreamingAssets" directory is copied to the the Application.persistentDataPath directory.
        /// [WebGL] The target file in the "StreamingAssets" directory is copied to the WebGL's virtual filesystem.
        /// </remarks>
        /// <param name="filepath">
        /// A file path relative to the "StreamingAssets" directory.
        /// </param>
        /// <param name="progressChanged">
        /// An optional callback that is called when the process is the progress. Returns the file path and a progress value (0.0 to 1.0).
        /// </param>
        /// <param name="errorOccurred">
        /// An optional callback that is called when the process is error occurred. Returns the file path and an error string and an error response code.
        /// </param>
        /// <param name="refresh">
        /// [Android][WebGL] If false, the file is not copied if it already exists. If true, the file is always copied.
        /// </param>
        /// <param name="timeout">
        /// [Android][WebGL] Sets the UnityWebRequest to abort after the specified number of seconds. If set to 0, no timeout is applied. The default is 0.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the download operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous download operation. The result is a readable file path where the downloaded file was saved, or <c>string.Empty</c> if the download fails.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="filepath"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this method is called from a non-main thread.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown if the download operation is canceled.
        /// </exception>
        public static async Task<string> GetFilePathTaskAsync(
            string filepath,
            Action<string, float> progressChanged = null,
            Action<string, string, long> errorOccurred = null,
            bool refresh = false,
            int timeout = 0,
            CancellationToken cancellationToken = default)
        {
            if (filepath == null)
                throw new ArgumentNullException(nameof(filepath));

            if (SynchronizationContext.Current == null)
                throw new InvalidOperationException("This method must be called from the main thread.");

            cancellationToken.ThrowIfCancellationRequested();

            filepath = filepath.TrimStart(chTrims);

            if (string.IsNullOrEmpty(filepath) || string.IsNullOrEmpty(Path.GetExtension(filepath)))
            {
                progressChanged?.Invoke(filepath, 0);
                await Task.Yield();

                cancellationToken.ThrowIfCancellationRequested();

                progressChanged?.Invoke(filepath, 1);
                errorOccurred?.Invoke(filepath, "Invalid file path.", -1);

                return string.Empty;
            }

#if (UNITY_ANDROID || UNITY_WEBGL) && !UNITY_EDITOR
            string destPath = await CopyFileTaskAsync(filepath, progressChanged, errorOccurred, refresh, timeout, cancellationToken);
            return destPath;
#else // (UNITY_ANDROID || UNITY_WEBGL) && !UNITY_EDITOR
            string destPath = Path.Combine(Application.streamingAssetsPath, filepath);

            progressChanged?.Invoke(filepath, 0);
            await Task.Yield();

            cancellationToken.ThrowIfCancellationRequested();

            progressChanged?.Invoke(filepath, 1);

            if (File.Exists(destPath))
            {
                return destPath;
            }
            else
            {
                errorOccurred?.Invoke(filepath, "File does not exist.", -1);
                return string.Empty;
            }
#endif // (UNITY_ANDROID || UNITY_WEBGL) && !UNITY_EDITOR
        }

        /// <summary>
        /// Asynchronously retrieves the multiple readable paths of files in the "StreamingAssets" directory using Task.
        /// </summary>
        /// <remarks>
        /// Provide a relative file path based on the "StreamingAssets" directory.  e.g., "foobar.txt" or "hogehoge/foobar.txt".
        /// [Android] The target file that exists in the "StreamingAssets" directory is copied to the the Application.persistentDataPath directory.
        /// [WebGL] The target file in the "StreamingAssets" directory is copied to the WebGL's virtual filesystem.
        /// </remarks>
        /// <param name="filepaths">
        /// The list of file paths relative to the "StreamingAssets" directory.
        /// </param>
        /// <param name="completed">
        /// An optional callback that is called when one process is completed. Returns a readable file path in case of success and returns empty in case of error.
        /// </param>
        /// <param name="progressChanged">
        /// An optional callback that is called when one process is the progress. Returns the file path and a progress value (0.0 to 1.0).
        /// </param>
        /// <param name="errorOccurred">
        /// An optional callback that is called when one process is error occurred. Returns the file path and an error string and an error response code.
        /// </param>
        /// <param name="refresh">
        /// [Android][WebGL] If false, the file is not copied if it already exists. If true, the file is always copied.
        /// </param>
        /// <param name="timeout">
        /// [Android][WebGL] Sets the UnityWebRequest to abort after the specified number of seconds. If set to 0, no timeout is applied. The default is 0.
        /// </param>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the download operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous download operation. The result is a list of readable file paths where the downloaded file was saved, or <c>string.Empty</c> if the download fails.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="filepath"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this method is called from a non-main thread.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Thrown if the download operation is canceled.
        /// </exception>
        public static async Task<IReadOnlyList<string>> GetMultipleFilePathsTaskAsync(
            IReadOnlyList<string> filepaths,
            Action<string> completed = null,
            Action<string, float> progressChanged = null,
            Action<string, string, long> errorOccurred = null,
            bool refresh = false,
            int timeout = 0,
            CancellationToken cancellationToken = default)
        {
            if (filepaths == null)
                throw new ArgumentNullException(nameof(filepaths));

            if (SynchronizationContext.Current == null)
                throw new InvalidOperationException("This method must be called from the main thread.");

            var downloadTasks = new List<Task<string>>(filepaths.Count);
            var results = new string[filepaths.Count];

            for (int i = 0; i < filepaths.Count; i++)
            {
                int index = i;

                var task = GetFilePathTaskAsync(
                    filepaths[index],
                    progressChanged,
                    errorOccurred,
                    refresh,
                    timeout,
                    cancellationToken
                ).ContinueWith(t =>
                {
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        var result = t.Result;
                        completed?.Invoke(result);
                        return result;
                    }
                    return string.Empty;

                }, cancellationToken, TaskContinuationOptions.AttachedToParent, TaskScheduler.FromCurrentSynchronizationContext());

                downloadTasks.Add(task);
            }

            results = await Task.WhenAll(downloadTasks);

            return results;
        }

        #endregion

        private static char[] chTrims = {
            '.',
#if UNITY_WINRT_8_1 && !UNITY_EDITOR
            '/',
            '\\'
#else
            System.IO.Path.DirectorySeparatorChar,
            System.IO.Path.AltDirectorySeparatorChar
#endif
        };
    }
}
