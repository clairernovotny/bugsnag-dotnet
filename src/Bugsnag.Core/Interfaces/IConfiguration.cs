﻿using System;

namespace Bugsnag.Core
{
    /// <summary>
    /// Defines the interface for setting and retrieving the configuration for a sending notifications to Bugsnag
    /// </summary>
    public interface IConfiguration
    {
        #region Application Settings
        /// <summary>
        /// Gets the API key used to send notifications to a specific Bugsnag account
        /// </summary>
        string ApiKey { get; }

        /// <summary>
        /// Gets or sets the application version
        /// </summary>
        string AppVersion { get; set; }

        /// <summary>
        /// Gets or sets the release stage of the application
        /// </summary>
        string ReleaseStage { get; set; }

        /// <summary>
        /// Specifies the release stages that notifications should be sent
        /// </summary>
        /// <param name="releaseStages">The stages to notify on</param>
        void SetNotifyReleaseStages(params string[] releaseStages);

        /// <summary>
        /// Resets any restrictions and notifies on all release stages
        /// </summary>
        void NotifyOnAllReleaseStages();

        /// <summary>
        /// Detects if the current release stage is a stage that should be notified on
        /// </summary>
        /// <returns>True if we should notify, otherwise false</returns>
        bool IsNotifyReleaseStage();

        /// <summary>
        /// Gets or sets the endpoint that defines where to send the notifications
        /// </summary>
        string Endpoint { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the notifications should be sent using SSL
        /// </summary>
        bool UseSsl { get; set; }

        /// <summary>
        /// Gets the endpoint URL that notifications will be send to
        /// </summary>
        Uri EndpointUrl { get; }
        #endregion

        #region Additional Notification Data
        /// <summary>
        /// Gets the unique identifier used to identify a user
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// Gets the users email
        /// </summary>
        string UserEmail { get; }

        /// <summary>
        /// Gets the users human readable name
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Gets the current logged in user name
        /// </summary>
        string LoggedOnUser { get; }

        /// <summary>
        /// Sets the information about the current user of the system
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="userEmail">The email of the user</param>
        /// <param name="userName">The name of the user</param>
        void SetUser(string userId, string userEmail, string userName);

        /// <summary>
        /// Gets or sets the context to apply to the subsequent notifications
        /// </summary>
        string Context { get; set; }

        /// <summary>
        /// Gets the metadata to send with every error report
        /// </summary>
        Metadata Metadata { get; }
        #endregion

        #region Notification Settings

        /// <summary>
        /// Sets the file prefixes that should be removed from frame file paths
        /// </summary>
        /// <param name="prefixes">The prefixes to remove</param>
        void SetFilePrefix(params string[] prefixes);

        /// <summary>
        /// Removes all file prefixes from a filename
        /// </summary>
        /// <param name="fileName">The filename to modify</param>
        /// <returns>The filename with the prefixes removed</returns>
        string RemoveFileNamePrefix(string fileName);

        /// <summary>
        /// Sets the project namespaces used to detect local method calls
        /// </summary>
        /// <param name="namespaces">The project namespaces</param>
        void SetProjectNamespaces(params string[] namespaces);

        /// <summary>
        /// Indicates if a method name belongs to In Project namespaces
        /// </summary>
        /// <param name="fullMethodName">The fully qualified method name</param>
        /// <returns>True if it belongs to one of the project namespaces, otherwise false</returns>
        bool IsInProjectNamespace(string fullMethodName);

        /// <summary>
        /// Gets or sets a value indicating whether we are auto detecting method calls as 
        /// in project calls in stack traces
        /// </summary>
        bool AutoDetectInProject { get; set; }

        Func<Event, bool> BeforeNotifyFunc { get; set; }

        bool IsClassToIgnore(string className);
        #endregion
    }
}