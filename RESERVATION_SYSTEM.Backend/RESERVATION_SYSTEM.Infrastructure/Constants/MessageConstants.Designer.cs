﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RESERVATION_SYSTEM.Infrastructure.Constants {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class MessageConstants {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MessageConstants() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("RESERVATION_SYSTEM.Infrastructure.Constants.MessageConstants", typeof(MessageConstants).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT [Id]
        ///      ,[Name]
        ///      ,[Email]
        ///      ,[Phone]
        ///      ,[DateRegistration]
        ///  FROM [Reservation].[dbo].[Customer]
        ///  {0}.
        /// </summary>
        internal static string GetCustomers {
            get {
                return ResourceManager.GetString("GetCustomers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select * from (
        /// SELECT 
        ///  h.[ReservationID]
        ///  ,h.[DateChange]
        ///  ,h.[DescriptionChange]
        ///  ,s.[Name] as ServiceName
        ///  ,c.[name] as CustomerName
        /// FROM 
        ///  [Reservation].[dbo].[HistoryReservation] as h
        /// inner join Reservation.dbo.Reservation as r on r.Id = h.ReservationID
        /// inner join Reservation.dbo.Customer as c on c.id = r.[CustomerID]
        /// inner join Reservation.dbo.[Service] as s on s.id = r.ServiceID
        ///) as s
        ///{0}.
        /// </summary>
        internal static string GetHistoryReservation {
            get {
                return ResourceManager.GetString("GetHistoryReservation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select * from (
        /// SELECT 
        ///  r.[Id]
        ///    ,r.[CustomerID]
        ///    ,c.[name] as CustomerName
        ///    ,r.[ServiceID]
        ///    ,s.[Name] as ServiceName
        ///    ,r.[DateReservation]
        ///    ,r.[StartDate]
        ///    ,r.[EndDate]
        ///    ,r.[State]
        ///    ,r.[NumberPeople]
        ///   FROM [Reservation].[dbo].[Reservation] as r
        ///   inner join Reservation.dbo.Customer as c on c.id = r.[CustomerID]
        ///   inner join Reservation.dbo.[Service] as s on s.id = r.ServiceID
        ///   where r.State != &apos;Canceled&apos;
        ///  ) as s
        ///  {0}.
        /// </summary>
        internal static string GetReservation {
            get {
                return ResourceManager.GetString("GetReservation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT  [Id]
        ///      ,[Name]
        ///      ,[Description]
        ///      ,[Price]
        ///      ,[Capacity]
        ///   ,[MaximumReservationTime]
        ///      ,[MinimumReservationTime]
        ///  FROM [Reservation].[dbo].[Service]
        ///{0}
        ///.
        /// </summary>
        internal static string GetServices {
            get {
                return ResourceManager.GetString("GetServices", resourceCulture);
            }
        }
    }
}