﻿namespace Cake.Prca.PullRequests.Tfs
{
    using System;
    using System.Linq;
    using Core.IO;
    using Microsoft.TeamFoundation.SourceControl.WebApi;

    /// <summary>
    /// Extensions for <see cref="GitPullRequestCommentThread"/>.
    /// </summary>
    internal static class GitPullRequestCommentThreadExtensions
    {
        private const string CommentSourcePropertyName = "CakePrcaCommentSource";
        private const string IssueMessagePropertyName = "CakePrcaIssueMessage";

        /// <summary>
        /// Converts a <see cref="GitPullRequestCommentThread"/> from TFS to a <see cref="IPrcaDiscussionThread"/> as used in this addin.
        /// </summary>
        /// <param name="thread">TFS thread to convert.</param>
        /// <returns>Converted thread.</returns>
        public static IPrcaDiscussionThread ToPrcaDiscussionThread(this GitPullRequestCommentThread thread)
        {
            thread.NotNull(nameof(thread));

            return new PrcaDiscussionThread(
                thread.Id,
                thread.Status.ToPrcaDiscussionStatus(),
                new FilePath(thread.ThreadContext.FilePath.TrimStart('/')),
                thread.Comments.Select(x => x.ToPrcaDiscussionComment()))
            {
                CommentSource = thread.GetCommentSource(),
            };
        }

        /// <summary>
        /// Gets the comment source value used to decorate comments created by this addin.
        /// </summary>
        /// <param name="thread">Thread to get the value from.</param>
        /// <returns>Comment source value.</returns>
        public static string GetCommentSource(this GitPullRequestCommentThread thread)
        {
            thread.NotNull(nameof(thread));

            return thread.Properties.GetValue(CommentSourcePropertyName, string.Empty);
        }

        /// <summary>
        /// Sets the comment source value used to decorate comments created by this addin.
        /// </summary>
        /// <param name="thread">Thread for which the value should be set.</param>
        /// <param name="value">Value to set as comment source.</param>
        public static void SetCommentSource(this GitPullRequestCommentThread thread, string value)
        {
            thread.NotNull(nameof(thread));

            thread.SetValue(CommentSourcePropertyName, value);
        }

        /// <summary>
        /// Checks if the custom comment source value used to decorate comments created by this addin
        /// has a specific value.
        /// </summary>
        /// <param name="thread">Thread to check.</param>
        /// <param name="value">Value to check for.</param>
        /// <returns><c>True</c> if the value is identical, <c>False</c> otherwise.</returns>
        public static bool IsCommentSource(this GitPullRequestCommentThread thread, string value)
        {
            thread.NotNull(nameof(thread));

            return thread.GetCommentSource() == value;
        }

        /// <summary>
        /// Gets the original message of the issue as provided by Cake.Prca,
        /// without any formatting done by this addin.
        /// </summary>
        /// <param name="thread">Thread to get the value from.</param>
        /// <returns>Original message of the issue.</returns>
        public static string GetIssueMessage(this GitPullRequestCommentThread thread)
        {
            thread.NotNull(nameof(thread));

            return thread.Properties.GetValue(IssueMessagePropertyName, string.Empty);
        }

        /// <summary>
        /// Sets the original message of the issue as provided by Cake.Prca.
        /// </summary>
        /// <param name="thread">Thread for which the value should be set.</param>
        /// <param name="value">Value to set as the original message.</param>
        public static void SetIssueMessage(this GitPullRequestCommentThread thread, string value)
        {
            thread.NotNull(nameof(thread));

            thread.SetValue(IssueMessagePropertyName, value);
        }

        /// <summary>
        /// Sets a value in the thread properties.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="thread">Thread for which the value should be set.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">Value to set.</param>
        private static void SetValue<T>(this GitPullRequestCommentThread thread, string propertyName, T value)
        {
            thread.NotNull(nameof(thread));
            propertyName.NotNullOrWhiteSpace(nameof(propertyName));

            if (thread.Properties == null)
            {
                throw new InvalidOperationException("Properties collection is not created.");
            }

            if (thread.Properties.ContainsKey(propertyName))
            {
                thread.Properties[propertyName] = value;
            }
            else
            {
                thread.Properties.Add(propertyName, value);
            }
        }
    }
}
