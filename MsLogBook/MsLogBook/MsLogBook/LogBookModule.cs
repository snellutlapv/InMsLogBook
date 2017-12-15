using System;
using System.Collections.Generic;
using Nancy;
using Nancy.Responses;
using PV.Data.Standard;
using PV.Data.Standard.EntityClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;
using PV.App.Managers.Standard.Helpers;
using PackageModels = PV.App.Managers.Standard.PackageModels;
using PV.Data.Standard.HelperClasses;

namespace MsLogBook
{
    public class LogBookModule : NancyModule
    {
        private const string MyNameSpace = "PV.Data.Standard.DataManagers";

        public LogBookModule()
        {
            Get["/GetLogBook/{lognum}&{env}"] = param =>
            {
                var lognum = param.lognum;
                var environment = param.env;

                var results = GetLogBook(lognum, environment);

                return new JsonResponse(results, new DefaultJsonSerializer());
            };

            Get["/GetLogBookWithDetails/{lognum}&{env}&{fetchWithDetails}"] = param =>
            {
                var lognum = param.lognum;
                var environment = param.env;
                var fetchWithDetails = param.fetchWithDetails;

                var results = GetLogBook(lognum, environment, fetchWithDetails);

                return new JsonResponse(results, new DefaultJsonSerializer());
            };

            Get["/GetClinicLogBooks/{clinicPk}&{visitDate}&{env}"] = param =>
            {
                var clinicPk = param.clinicPk;
                var visitDate = param.visitDate;
                var environment = param.env;

                var results = GetClinicLogBooks(clinicPk, visitDate, environment);

                return new JsonResponse(results, new DefaultJsonSerializer());
            };
        }

        private LogBookDataModel GetLogBook(int logNum, string environment, bool fetchWithDetails = false)
        {
            using (var adapter = AdapterFactory.CreateAdapter(MyNameSpace, environment))
            {
                return GetLogBookWithDetails(logNum, adapter, fetchWithDetails);
            }
        }

        private LogBookDataModel GetLogBookWithDetails(int logNum, IDataAccessAdapter adapter, bool fetchWithDetails = false)
        {
            try
            {
                var toFetch = new LogBookEntity(logNum);
                var path = new PrefetchPath2((int)EntityType.LogBookEntity);

                if (fetchWithDetails)
                {
                    path.Add(LogBookEntity.PrefetchPathLogDetails);
                }

                var fetchResult = adapter.FetchEntity(toFetch, path);

                if (!fetchResult)
                {
                    return null;
                }

                var logBookModel = TransformLogBookEntityToModel(toFetch);

                logBookModel.LogDetailModel = new List<PackageModels.LogDetailModel>();

                foreach (var logDetail in toFetch.LogDetails)
                {
                    var logDetailModel = TransformLogDetailEntityToModel(logDetail);

                    logBookModel.LogDetailModel.Add(logDetailModel);
                }

                return logBookModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private LogBookDataModel TransformLogBookEntityToModel(LogBookEntity entity)
        {
            var logBook = new LogBookDataModel
            {
                Clinic = entity.Clinic,
                AppointmentId = 0,
                CheckInPrintOutDateTime = entity.CheckInPrintOutDateTime,
                CheckInPrintUserName = entity.CheckInPrintUserName,
                CheckInPrintUserProfilePk = entity.CheckInPrintUserProfilePk,
                EpsStatus = entity.EpsStatus,
                eRegStatus = entity.ERegStatus,
                FirstName = entity.FirstName,
                KioskPIN = entity.KioskPin,
                KioskSessionPk = entity.KioskSessionPk,
                KioskStatus = entity.KioskStatus,
                LastName = entity.LastName,
                LogNumber = entity.LogNum,
                MiscStatus = entity.MiscStatus,
                PatientNumber = entity.PatNum,
                PrivateStatus = entity.PrvStatus,
                ScheduledMethod = entity.ScheduledMethod,
                ScheduledTime = entity.ScheduledTime,
                ServiceDate = entity.SvcDate,
                TimeIn = entity.TimeIn
            };

            return logBook;
        }

        private PackageModels.LogDetailModel TransformLogDetailEntityToModel(LogDetailEntity entity)
        {
            var logDetail = new PackageModels.LogDetailModel
            {
                AppointmentId = entity.AppointmentId,
                Clinic = entity.Clinic,
                CmpName = entity.CmpName,
                CmpNum = entity.CmpNum,
                LogNum = entity.LogNum,
                Notes = entity.Notes,
                PatNum = entity.PatNum,
                PhyName = entity.PhyName,
                PhyNum = entity.PhyNum,
                Practice = entity.Practice,
                QuickVisitFlag = entity.QuickVisitFlag,
                QuickVisitType = entity.QuickVisitType,
                SelfPayFlag = entity.SelfPayFlag,
                Status = entity.Status,
                TimeIn = entity.TimeIn,
                UserId = entity.CrtUserId,
                UserNum = entity.LastUpdUserNum,
                Type = entity.VisitType,
                SvcDate = entity.SvcDate
            };

            return logDetail;
        }

        private EntityCollection<ClinicLogBookEntity> GetClinicLogBooks(Guid clinicPk, DateTime visitDate, string environment)
        {
            var filter = new RelationPredicateBucket();
            filter.PredicateExpression.Add(ClinicLogBookFields.ClinicPk == clinicPk);
            filter.PredicateExpression.AddWithAnd(ClinicLogBookFields.SvcDate == visitDate.Date);

            var myCollection = new EntityCollection<ClinicLogBookEntity>();

            try
            {
                using (IDataAccessAdapter adapter = AdapterFactory.CreateAdapter(MyNameSpace, environment))
                {
                    adapter.FetchEntityCollection(myCollection, filter, 0, null, null, null);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return myCollection;
        }
    }
}
