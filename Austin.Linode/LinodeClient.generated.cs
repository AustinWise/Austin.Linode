﻿
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Austin.Linode
{
    partial class LinodeClient
    {
        public readonly Version GeneratedApiVersion = new Version(3, 3, 0, 0);
        public void Account_EstimateInvoice(
            string mode,
            int? LinodeID = null,
            int? PaymentTerm = null,
            int? PlanID = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("mode", mode);
            if (LinodeID != null)
                myParams.Add("LinodeID", LinodeID.Value.ToString(CultureInfo.InvariantCulture));
            if (PaymentTerm != null)
                myParams.Add("PaymentTerm", PaymentTerm.Value.ToString(CultureInfo.InvariantCulture));
            if (PlanID != null)
                myParams.Add("PlanID", PlanID.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("account.estimateinvoice", myParams);
        }
        public void Account_Info(
        )
        {
            GetResponse<object>("account.info", null);
        }
        public void Account_PayBalance(
        )
        {
            GetResponse<object>("account.paybalance", null);
        }
        public void Account_UpdateCard(
            int ccExpMonth,
            int ccExpYear,
            int ccNumber)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("ccExpMonth", ccExpMonth.ToString(CultureInfo.InvariantCulture));
            myParams.Add("ccExpYear", ccExpYear.ToString(CultureInfo.InvariantCulture));
            myParams.Add("ccNumber", ccNumber.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("account.updatecard", myParams);
        }
        public Austin.Linode.DataCenter[] Avail_Datacenters(
        )
        {
            return GetResponse<Austin.Linode.DataCenter[]>("avail.datacenters", null);
        }
        public void Avail_Distributions(
            int? DistributionID = null)
        {
            var myParams = new Dictionary<string, string>();
            if (DistributionID != null)
                myParams.Add("DistributionID", DistributionID.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("avail.distributions", myParams);
        }
        public void Avail_Kernels(
            bool? isXen = null,
            int? KernelID = null)
        {
            var myParams = new Dictionary<string, string>();
            if (isXen != null)
                myParams.Add("isXen", isXen.Value ? "true" : "false");
            if (KernelID != null)
                myParams.Add("KernelID", KernelID.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("avail.kernels", myParams);
        }
        public Austin.Linode.Plan[] Avail_LinodePlans(
            int? PlanID = null)
        {
            var myParams = new Dictionary<string, string>();
            if (PlanID != null)
                myParams.Add("PlanID", PlanID.Value.ToString(CultureInfo.InvariantCulture));
            return GetResponse<Austin.Linode.Plan[]>("avail.linodeplans", myParams);
        }
        public void Avail_StackScripts(
            int? DistributionID = null,
            string DistributionVendor = null,
            string keywords = null)
        {
            var myParams = new Dictionary<string, string>();
            if (DistributionID != null)
                myParams.Add("DistributionID", DistributionID.Value.ToString(CultureInfo.InvariantCulture));
            if (DistributionVendor != null)
                myParams.Add("DistributionVendor", DistributionVendor);
            if (keywords != null)
                myParams.Add("keywords", keywords);
            GetResponse<object>("avail.stackscripts", myParams);
        }
        public void Domain_Create(
            string Domain,
            string Type,
            string axfr_ips = null,
            string Description = null,
            int? Expire_sec = null,
            string lpm_displayGroup = null,
            string master_ips = null,
            int? Refresh_sec = null,
            int? Retry_sec = null,
            string SOA_Email = null,
            int? status = null,
            int? TTL_sec = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("Domain", Domain);
            myParams.Add("Type", Type);
            if (axfr_ips != null)
                myParams.Add("axfr_ips", axfr_ips);
            if (Description != null)
                myParams.Add("Description", Description);
            if (Expire_sec != null)
                myParams.Add("Expire_sec", Expire_sec.Value.ToString(CultureInfo.InvariantCulture));
            if (lpm_displayGroup != null)
                myParams.Add("lpm_displayGroup", lpm_displayGroup);
            if (master_ips != null)
                myParams.Add("master_ips", master_ips);
            if (Refresh_sec != null)
                myParams.Add("Refresh_sec", Refresh_sec.Value.ToString(CultureInfo.InvariantCulture));
            if (Retry_sec != null)
                myParams.Add("Retry_sec", Retry_sec.Value.ToString(CultureInfo.InvariantCulture));
            if (SOA_Email != null)
                myParams.Add("SOA_Email", SOA_Email);
            if (status != null)
                myParams.Add("status", status.Value.ToString(CultureInfo.InvariantCulture));
            if (TTL_sec != null)
                myParams.Add("TTL_sec", TTL_sec.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("domain.create", myParams);
        }
        public void Domain_Delete(
            int DomainID)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("DomainID", DomainID.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("domain.delete", myParams);
        }
        public void Domain_List(
            int? DomainID = null)
        {
            var myParams = new Dictionary<string, string>();
            if (DomainID != null)
                myParams.Add("DomainID", DomainID.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("domain.list", myParams);
        }
        public void Domain_Resource_Create(
            int DomainID,
            string Type,
            string Name = null,
            int? Port = null,
            int? Priority = null,
            string Protocol = null,
            string Target = null,
            int? TTL_sec = null,
            int? Weight = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("DomainID", DomainID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("Type", Type);
            if (Name != null)
                myParams.Add("Name", Name);
            if (Port != null)
                myParams.Add("Port", Port.Value.ToString(CultureInfo.InvariantCulture));
            if (Priority != null)
                myParams.Add("Priority", Priority.Value.ToString(CultureInfo.InvariantCulture));
            if (Protocol != null)
                myParams.Add("Protocol", Protocol);
            if (Target != null)
                myParams.Add("Target", Target);
            if (TTL_sec != null)
                myParams.Add("TTL_sec", TTL_sec.Value.ToString(CultureInfo.InvariantCulture));
            if (Weight != null)
                myParams.Add("Weight", Weight.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("domain.resource.create", myParams);
        }
        public void Domain_Resource_Delete(
            int DomainID,
            int ResourceID)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("DomainID", DomainID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("ResourceID", ResourceID.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("domain.resource.delete", myParams);
        }
        public void Domain_Resource_List(
            int DomainID,
            int? ResourceID = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("DomainID", DomainID.ToString(CultureInfo.InvariantCulture));
            if (ResourceID != null)
                myParams.Add("ResourceID", ResourceID.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("domain.resource.list", myParams);
        }
        public void Domain_Resource_Update(
            int ResourceID,
            int? DomainID = null,
            string Name = null,
            int? Port = null,
            int? Priority = null,
            string Protocol = null,
            string Target = null,
            int? TTL_sec = null,
            int? Weight = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("ResourceID", ResourceID.ToString(CultureInfo.InvariantCulture));
            if (DomainID != null)
                myParams.Add("DomainID", DomainID.Value.ToString(CultureInfo.InvariantCulture));
            if (Name != null)
                myParams.Add("Name", Name);
            if (Port != null)
                myParams.Add("Port", Port.Value.ToString(CultureInfo.InvariantCulture));
            if (Priority != null)
                myParams.Add("Priority", Priority.Value.ToString(CultureInfo.InvariantCulture));
            if (Protocol != null)
                myParams.Add("Protocol", Protocol);
            if (Target != null)
                myParams.Add("Target", Target);
            if (TTL_sec != null)
                myParams.Add("TTL_sec", TTL_sec.Value.ToString(CultureInfo.InvariantCulture));
            if (Weight != null)
                myParams.Add("Weight", Weight.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("domain.resource.update", myParams);
        }
        public void Domain_Update(
            int DomainID,
            string axfr_ips = null,
            string Description = null,
            string Domain = null,
            int? Expire_sec = null,
            string lpm_displayGroup = null,
            string master_ips = null,
            int? Refresh_sec = null,
            int? Retry_sec = null,
            string SOA_Email = null,
            int? status = null,
            int? TTL_sec = null,
            string Type = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("DomainID", DomainID.ToString(CultureInfo.InvariantCulture));
            if (axfr_ips != null)
                myParams.Add("axfr_ips", axfr_ips);
            if (Description != null)
                myParams.Add("Description", Description);
            if (Domain != null)
                myParams.Add("Domain", Domain);
            if (Expire_sec != null)
                myParams.Add("Expire_sec", Expire_sec.Value.ToString(CultureInfo.InvariantCulture));
            if (lpm_displayGroup != null)
                myParams.Add("lpm_displayGroup", lpm_displayGroup);
            if (master_ips != null)
                myParams.Add("master_ips", master_ips);
            if (Refresh_sec != null)
                myParams.Add("Refresh_sec", Refresh_sec.Value.ToString(CultureInfo.InvariantCulture));
            if (Retry_sec != null)
                myParams.Add("Retry_sec", Retry_sec.Value.ToString(CultureInfo.InvariantCulture));
            if (SOA_Email != null)
                myParams.Add("SOA_Email", SOA_Email);
            if (status != null)
                myParams.Add("status", status.Value.ToString(CultureInfo.InvariantCulture));
            if (TTL_sec != null)
                myParams.Add("TTL_sec", TTL_sec.Value.ToString(CultureInfo.InvariantCulture));
            if (Type != null)
                myParams.Add("Type", Type);
            GetResponse<object>("domain.update", myParams);
        }
        public void Linode_Boot(
            int LinodeID,
            int? ConfigID = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            if (ConfigID != null)
                myParams.Add("ConfigID", ConfigID.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("linode.boot", myParams);
        }
        public void Linode_Clone(
            int DatacenterID,
            int LinodeID,
            int PlanID,
            int? PaymentTerm = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("DatacenterID", DatacenterID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("PlanID", PlanID.ToString(CultureInfo.InvariantCulture));
            if (PaymentTerm != null)
                myParams.Add("PaymentTerm", PaymentTerm.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("linode.clone", myParams);
        }
        public void Linode_Config_Create(
            int KernelID,
            string Label,
            int LinodeID,
            string Comments = null,
            bool? devtmpfs_automount = null,
            string DiskList = null,
            bool? helper_depmod = null,
            bool? helper_disableUpdateDB = null,
            bool? helper_xen = null,
            int? RAMLimit = null,
            string RootDeviceCustom = null,
            int? RootDeviceNum = null,
            bool? RootDeviceRO = null,
            string RunLevel = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("KernelID", KernelID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("Label", Label);
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            if (Comments != null)
                myParams.Add("Comments", Comments);
            if (devtmpfs_automount != null)
                myParams.Add("devtmpfs_automount", devtmpfs_automount.Value ? "true" : "false");
            if (DiskList != null)
                myParams.Add("DiskList", DiskList);
            if (helper_depmod != null)
                myParams.Add("helper_depmod", helper_depmod.Value ? "true" : "false");
            if (helper_disableUpdateDB != null)
                myParams.Add("helper_disableUpdateDB", helper_disableUpdateDB.Value ? "true" : "false");
            if (helper_xen != null)
                myParams.Add("helper_xen", helper_xen.Value ? "true" : "false");
            if (RAMLimit != null)
                myParams.Add("RAMLimit", RAMLimit.Value.ToString(CultureInfo.InvariantCulture));
            if (RootDeviceCustom != null)
                myParams.Add("RootDeviceCustom", RootDeviceCustom);
            if (RootDeviceNum != null)
                myParams.Add("RootDeviceNum", RootDeviceNum.Value.ToString(CultureInfo.InvariantCulture));
            if (RootDeviceRO != null)
                myParams.Add("RootDeviceRO", RootDeviceRO.Value ? "true" : "false");
            if (RunLevel != null)
                myParams.Add("RunLevel", RunLevel);
            GetResponse<object>("linode.config.create", myParams);
        }
        public void Linode_Config_Delete(
            int ConfigID,
            int LinodeID)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("ConfigID", ConfigID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("linode.config.delete", myParams);
        }
        public void Linode_Config_List(
            int LinodeID,
            int? ConfigID = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            if (ConfigID != null)
                myParams.Add("ConfigID", ConfigID.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("linode.config.list", myParams);
        }
        public void Linode_Config_Update(
            int ConfigID,
            string Comments = null,
            bool? devtmpfs_automount = null,
            string DiskList = null,
            bool? helper_depmod = null,
            bool? helper_disableUpdateDB = null,
            bool? helper_xen = null,
            int? KernelID = null,
            string Label = null,
            int? LinodeID = null,
            int? RAMLimit = null,
            string RootDeviceCustom = null,
            int? RootDeviceNum = null,
            bool? RootDeviceRO = null,
            string RunLevel = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("ConfigID", ConfigID.ToString(CultureInfo.InvariantCulture));
            if (Comments != null)
                myParams.Add("Comments", Comments);
            if (devtmpfs_automount != null)
                myParams.Add("devtmpfs_automount", devtmpfs_automount.Value ? "true" : "false");
            if (DiskList != null)
                myParams.Add("DiskList", DiskList);
            if (helper_depmod != null)
                myParams.Add("helper_depmod", helper_depmod.Value ? "true" : "false");
            if (helper_disableUpdateDB != null)
                myParams.Add("helper_disableUpdateDB", helper_disableUpdateDB.Value ? "true" : "false");
            if (helper_xen != null)
                myParams.Add("helper_xen", helper_xen.Value ? "true" : "false");
            if (KernelID != null)
                myParams.Add("KernelID", KernelID.Value.ToString(CultureInfo.InvariantCulture));
            if (Label != null)
                myParams.Add("Label", Label);
            if (LinodeID != null)
                myParams.Add("LinodeID", LinodeID.Value.ToString(CultureInfo.InvariantCulture));
            if (RAMLimit != null)
                myParams.Add("RAMLimit", RAMLimit.Value.ToString(CultureInfo.InvariantCulture));
            if (RootDeviceCustom != null)
                myParams.Add("RootDeviceCustom", RootDeviceCustom);
            if (RootDeviceNum != null)
                myParams.Add("RootDeviceNum", RootDeviceNum.Value.ToString(CultureInfo.InvariantCulture));
            if (RootDeviceRO != null)
                myParams.Add("RootDeviceRO", RootDeviceRO.Value ? "true" : "false");
            if (RunLevel != null)
                myParams.Add("RunLevel", RunLevel);
            GetResponse<object>("linode.config.update", myParams);
        }
        public void Linode_Create(
            int DatacenterID,
            int PlanID,
            int? PaymentTerm = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("DatacenterID", DatacenterID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("PlanID", PlanID.ToString(CultureInfo.InvariantCulture));
            if (PaymentTerm != null)
                myParams.Add("PaymentTerm", PaymentTerm.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("linode.create", myParams);
        }
        public void Linode_Delete(
            int LinodeID,
            bool? skipChecks = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            if (skipChecks != null)
                myParams.Add("skipChecks", skipChecks.Value ? "true" : "false");
            GetResponse<object>("linode.delete", myParams);
        }
        public void Linode_Disk_Create(
            string Label,
            int LinodeID,
            int Size,
            string Type)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("Label", Label);
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("Size", Size.ToString(CultureInfo.InvariantCulture));
            myParams.Add("Type", Type);
            GetResponse<object>("linode.disk.create", myParams);
        }
        public void Linode_Disk_CreateFromDistribution(
            int DistributionID,
            string Label,
            int LinodeID,
            string rootPass,
            int Size,
            string rootSSHKey = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("DistributionID", DistributionID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("Label", Label);
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("rootPass", rootPass);
            myParams.Add("Size", Size.ToString(CultureInfo.InvariantCulture));
            if (rootSSHKey != null)
                myParams.Add("rootSSHKey", rootSSHKey);
            GetResponse<object>("linode.disk.createfromdistribution", myParams);
        }
        public void Linode_Disk_CreateFromStackScript(
            int DistributionID,
            string Label,
            int LinodeID,
            string rootPass,
            int Size,
            int StackScriptID,
            string StackScriptUDFResponses)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("DistributionID", DistributionID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("Label", Label);
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("rootPass", rootPass);
            myParams.Add("Size", Size.ToString(CultureInfo.InvariantCulture));
            myParams.Add("StackScriptID", StackScriptID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("StackScriptUDFResponses", StackScriptUDFResponses);
            GetResponse<object>("linode.disk.createfromstackscript", myParams);
        }
        public void Linode_Disk_Delete(
            int DiskID,
            int LinodeID)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("DiskID", DiskID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("linode.disk.delete", myParams);
        }
        public void Linode_Disk_Duplicate(
            int DiskID,
            int LinodeID)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("DiskID", DiskID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("linode.disk.duplicate", myParams);
        }
        public void Linode_Disk_List(
            int LinodeID,
            int? DiskID = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            if (DiskID != null)
                myParams.Add("DiskID", DiskID.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("linode.disk.list", myParams);
        }
        public void Linode_Disk_Resize(
            int DiskID,
            int LinodeID,
            int size)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("DiskID", DiskID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("size", size.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("linode.disk.resize", myParams);
        }
        public void Linode_Disk_Update(
            int DiskID,
            bool? isReadOnly = null,
            string Label = null,
            int? LinodeID = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("DiskID", DiskID.ToString(CultureInfo.InvariantCulture));
            if (isReadOnly != null)
                myParams.Add("isReadOnly", isReadOnly.Value ? "true" : "false");
            if (Label != null)
                myParams.Add("Label", Label);
            if (LinodeID != null)
                myParams.Add("LinodeID", LinodeID.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("linode.disk.update", myParams);
        }
        public void Linode_Ip_AddPrivate(
            int LinodeID)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("linode.ip.addprivate", myParams);
        }
        public void Linode_Ip_List(
            int LinodeID,
            int? IPAddressID = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            if (IPAddressID != null)
                myParams.Add("IPAddressID", IPAddressID.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("linode.ip.list", myParams);
        }
        public Austin.Linode.Job[] Linode_Job_List(
            int LinodeID,
            int? JobID = null,
            bool? pendingOnly = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            if (JobID != null)
                myParams.Add("JobID", JobID.Value.ToString(CultureInfo.InvariantCulture));
            if (pendingOnly != null)
                myParams.Add("pendingOnly", pendingOnly.Value ? "true" : "false");
            return GetResponse<Austin.Linode.Job[]>("linode.job.list", myParams);
        }
        public Austin.Linode.Node[] Linode_List(
            int? LinodeID = null)
        {
            var myParams = new Dictionary<string, string>();
            if (LinodeID != null)
                myParams.Add("LinodeID", LinodeID.Value.ToString(CultureInfo.InvariantCulture));
            return GetResponse<Austin.Linode.Node[]>("linode.list", myParams);
        }
        public void Linode_Mutate(
            int LinodeID)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("linode.mutate", myParams);
        }
        public Austin.Linode.JobIdResponse Linode_Reboot(
            int LinodeID,
            int? ConfigID = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            if (ConfigID != null)
                myParams.Add("ConfigID", ConfigID.Value.ToString(CultureInfo.InvariantCulture));
            return GetResponse<Austin.Linode.JobIdResponse>("linode.reboot", myParams);
        }
        public void Linode_Resize(
            int LinodeID,
            int PlanID)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("PlanID", PlanID.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("linode.resize", myParams);
        }
        public void Linode_Shutdown(
            int LinodeID)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("linode.shutdown", myParams);
        }
        public void Linode_Update(
            int LinodeID,
            bool? Alert_bwin_enabled = null,
            int? Alert_bwin_threshold = null,
            bool? Alert_bwout_enabled = null,
            int? Alert_bwout_threshold = null,
            bool? Alert_bwquota_enabled = null,
            int? Alert_bwquota_threshold = null,
            bool? Alert_cpu_enabled = null,
            int? Alert_cpu_threshold = null,
            bool? Alert_diskio_enabled = null,
            int? Alert_diskio_threshold = null,
            int? backupWeeklyDay = null,
            int? backupWindow = null,
            string Label = null,
            string lpm_displayGroup = null,
            bool? ms_ssh_disabled = null,
            string ms_ssh_ip = null,
            int? ms_ssh_port = null,
            string ms_ssh_user = null,
            bool? watchdog = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            if (Alert_bwin_enabled != null)
                myParams.Add("Alert_bwin_enabled", Alert_bwin_enabled.Value ? "true" : "false");
            if (Alert_bwin_threshold != null)
                myParams.Add("Alert_bwin_threshold", Alert_bwin_threshold.Value.ToString(CultureInfo.InvariantCulture));
            if (Alert_bwout_enabled != null)
                myParams.Add("Alert_bwout_enabled", Alert_bwout_enabled.Value ? "true" : "false");
            if (Alert_bwout_threshold != null)
                myParams.Add("Alert_bwout_threshold", Alert_bwout_threshold.Value.ToString(CultureInfo.InvariantCulture));
            if (Alert_bwquota_enabled != null)
                myParams.Add("Alert_bwquota_enabled", Alert_bwquota_enabled.Value ? "true" : "false");
            if (Alert_bwquota_threshold != null)
                myParams.Add("Alert_bwquota_threshold", Alert_bwquota_threshold.Value.ToString(CultureInfo.InvariantCulture));
            if (Alert_cpu_enabled != null)
                myParams.Add("Alert_cpu_enabled", Alert_cpu_enabled.Value ? "true" : "false");
            if (Alert_cpu_threshold != null)
                myParams.Add("Alert_cpu_threshold", Alert_cpu_threshold.Value.ToString(CultureInfo.InvariantCulture));
            if (Alert_diskio_enabled != null)
                myParams.Add("Alert_diskio_enabled", Alert_diskio_enabled.Value ? "true" : "false");
            if (Alert_diskio_threshold != null)
                myParams.Add("Alert_diskio_threshold", Alert_diskio_threshold.Value.ToString(CultureInfo.InvariantCulture));
            if (backupWeeklyDay != null)
                myParams.Add("backupWeeklyDay", backupWeeklyDay.Value.ToString(CultureInfo.InvariantCulture));
            if (backupWindow != null)
                myParams.Add("backupWindow", backupWindow.Value.ToString(CultureInfo.InvariantCulture));
            if (Label != null)
                myParams.Add("Label", Label);
            if (lpm_displayGroup != null)
                myParams.Add("lpm_displayGroup", lpm_displayGroup);
            if (ms_ssh_disabled != null)
                myParams.Add("ms_ssh_disabled", ms_ssh_disabled.Value ? "true" : "false");
            if (ms_ssh_ip != null)
                myParams.Add("ms_ssh_ip", ms_ssh_ip);
            if (ms_ssh_port != null)
                myParams.Add("ms_ssh_port", ms_ssh_port.Value.ToString(CultureInfo.InvariantCulture));
            if (ms_ssh_user != null)
                myParams.Add("ms_ssh_user", ms_ssh_user);
            if (watchdog != null)
                myParams.Add("watchdog", watchdog.Value ? "true" : "false");
            GetResponse<object>("linode.update", myParams);
        }
        public void Linode_WebConsoleToken(
            int LinodeID)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("LinodeID", LinodeID.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("linode.webconsoletoken", myParams);
        }
        public void NodeBalancer_Config_Create(
            int NodeBalancerID,
            string Algorithm = null,
            string check = null,
            string check_attempts = null,
            string check_body = null,
            int? check_interval = null,
            string check_path = null,
            string check_timeout = null,
            int? Port = null,
            string Protocol = null,
            string ssl_cert = null,
            string ssl_key = null,
            string Stickiness = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("NodeBalancerID", NodeBalancerID.ToString(CultureInfo.InvariantCulture));
            if (Algorithm != null)
                myParams.Add("Algorithm", Algorithm);
            if (check != null)
                myParams.Add("check", check);
            if (check_attempts != null)
                myParams.Add("check_attempts", check_attempts);
            if (check_body != null)
                myParams.Add("check_body", check_body);
            if (check_interval != null)
                myParams.Add("check_interval", check_interval.Value.ToString(CultureInfo.InvariantCulture));
            if (check_path != null)
                myParams.Add("check_path", check_path);
            if (check_timeout != null)
                myParams.Add("check_timeout", check_timeout);
            if (Port != null)
                myParams.Add("Port", Port.Value.ToString(CultureInfo.InvariantCulture));
            if (Protocol != null)
                myParams.Add("Protocol", Protocol);
            if (ssl_cert != null)
                myParams.Add("ssl_cert", ssl_cert);
            if (ssl_key != null)
                myParams.Add("ssl_key", ssl_key);
            if (Stickiness != null)
                myParams.Add("Stickiness", Stickiness);
            GetResponse<object>("nodebalancer.config.create", myParams);
        }
        public void NodeBalancer_Config_Delete(
            int ConfigID,
            int NodeBalancerID)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("ConfigID", ConfigID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("NodeBalancerID", NodeBalancerID.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("nodebalancer.config.delete", myParams);
        }
        public void NodeBalancer_Config_List(
            int NodeBalancerID,
            int? ConfigID = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("NodeBalancerID", NodeBalancerID.ToString(CultureInfo.InvariantCulture));
            if (ConfigID != null)
                myParams.Add("ConfigID", ConfigID.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("nodebalancer.config.list", myParams);
        }
        public void NodeBalancer_Config_Update(
            int ConfigID,
            string Algorithm = null,
            string check = null,
            string check_attempts = null,
            string check_body = null,
            int? check_interval = null,
            string check_path = null,
            string check_timeout = null,
            int? Port = null,
            string Protocol = null,
            string ssl_cert = null,
            string ssl_key = null,
            string Stickiness = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("ConfigID", ConfigID.ToString(CultureInfo.InvariantCulture));
            if (Algorithm != null)
                myParams.Add("Algorithm", Algorithm);
            if (check != null)
                myParams.Add("check", check);
            if (check_attempts != null)
                myParams.Add("check_attempts", check_attempts);
            if (check_body != null)
                myParams.Add("check_body", check_body);
            if (check_interval != null)
                myParams.Add("check_interval", check_interval.Value.ToString(CultureInfo.InvariantCulture));
            if (check_path != null)
                myParams.Add("check_path", check_path);
            if (check_timeout != null)
                myParams.Add("check_timeout", check_timeout);
            if (Port != null)
                myParams.Add("Port", Port.Value.ToString(CultureInfo.InvariantCulture));
            if (Protocol != null)
                myParams.Add("Protocol", Protocol);
            if (ssl_cert != null)
                myParams.Add("ssl_cert", ssl_cert);
            if (ssl_key != null)
                myParams.Add("ssl_key", ssl_key);
            if (Stickiness != null)
                myParams.Add("Stickiness", Stickiness);
            GetResponse<object>("nodebalancer.config.update", myParams);
        }
        public void NodeBalancer_Create(
            int DatacenterID,
            int PaymentTerm,
            int? ClientConnThrottle = null,
            string Label = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("DatacenterID", DatacenterID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("PaymentTerm", PaymentTerm.ToString(CultureInfo.InvariantCulture));
            if (ClientConnThrottle != null)
                myParams.Add("ClientConnThrottle", ClientConnThrottle.Value.ToString(CultureInfo.InvariantCulture));
            if (Label != null)
                myParams.Add("Label", Label);
            GetResponse<object>("nodebalancer.create", myParams);
        }
        public void NodeBalancer_Delete(
            int NodeBalancerID)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("NodeBalancerID", NodeBalancerID.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("nodebalancer.delete", myParams);
        }
        public void NodeBalancer_List(
            int? NodeBalancerID = null)
        {
            var myParams = new Dictionary<string, string>();
            if (NodeBalancerID != null)
                myParams.Add("NodeBalancerID", NodeBalancerID.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("nodebalancer.list", myParams);
        }
        public void NodeBalancer_Node_Create(
            string Address,
            int ConfigID,
            string Label,
            string Mode = null,
            int? Weight = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("Address", Address);
            myParams.Add("ConfigID", ConfigID.ToString(CultureInfo.InvariantCulture));
            myParams.Add("Label", Label);
            if (Mode != null)
                myParams.Add("Mode", Mode);
            if (Weight != null)
                myParams.Add("Weight", Weight.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("nodebalancer.node.create", myParams);
        }
        public void NodeBalancer_Node_Delete(
            int NodeID)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("NodeID", NodeID.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("nodebalancer.node.delete", myParams);
        }
        public void NodeBalancer_Node_List(
            int ConfigID,
            int? NodeID = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("ConfigID", ConfigID.ToString(CultureInfo.InvariantCulture));
            if (NodeID != null)
                myParams.Add("NodeID", NodeID.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("nodebalancer.node.list", myParams);
        }
        public void NodeBalancer_Node_Update(
            int NodeID,
            string Address = null,
            string Label = null,
            string Mode = null,
            int? Weight = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("NodeID", NodeID.ToString(CultureInfo.InvariantCulture));
            if (Address != null)
                myParams.Add("Address", Address);
            if (Label != null)
                myParams.Add("Label", Label);
            if (Mode != null)
                myParams.Add("Mode", Mode);
            if (Weight != null)
                myParams.Add("Weight", Weight.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("nodebalancer.node.update", myParams);
        }
        public void NodeBalancer_Update(
            int NodeBalancerID,
            int? ClientConnThrottle = null,
            string Label = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("NodeBalancerID", NodeBalancerID.ToString(CultureInfo.InvariantCulture));
            if (ClientConnThrottle != null)
                myParams.Add("ClientConnThrottle", ClientConnThrottle.Value.ToString(CultureInfo.InvariantCulture));
            if (Label != null)
                myParams.Add("Label", Label);
            GetResponse<object>("nodebalancer.update", myParams);
        }
        public void StackScript_Create(
            string DistributionIDList,
            string Label,
            string script,
            string Description = null,
            bool? isPublic = null,
            string rev_note = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("DistributionIDList", DistributionIDList);
            myParams.Add("Label", Label);
            myParams.Add("script", script);
            if (Description != null)
                myParams.Add("Description", Description);
            if (isPublic != null)
                myParams.Add("isPublic", isPublic.Value ? "true" : "false");
            if (rev_note != null)
                myParams.Add("rev_note", rev_note);
            GetResponse<object>("stackscript.create", myParams);
        }
        public void StackScript_Delete(
            int StackScriptID)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("StackScriptID", StackScriptID.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("stackscript.delete", myParams);
        }
        public void StackScript_List(
            int? StackScriptID = null)
        {
            var myParams = new Dictionary<string, string>();
            if (StackScriptID != null)
                myParams.Add("StackScriptID", StackScriptID.Value.ToString(CultureInfo.InvariantCulture));
            GetResponse<object>("stackscript.list", myParams);
        }
        public void StackScript_Update(
            int StackScriptID,
            string Description = null,
            string DistributionIDList = null,
            bool? isPublic = null,
            string Label = null,
            string rev_note = null,
            string script = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("StackScriptID", StackScriptID.ToString(CultureInfo.InvariantCulture));
            if (Description != null)
                myParams.Add("Description", Description);
            if (DistributionIDList != null)
                myParams.Add("DistributionIDList", DistributionIDList);
            if (isPublic != null)
                myParams.Add("isPublic", isPublic.Value ? "true" : "false");
            if (Label != null)
                myParams.Add("Label", Label);
            if (rev_note != null)
                myParams.Add("rev_note", rev_note);
            if (script != null)
                myParams.Add("script", script);
            GetResponse<object>("stackscript.update", myParams);
        }
        public void Test_Echo(
        )
        {
            GetResponse<object>("test.echo", null);
        }
        public void User_GetApiKey(
            string password,
            string username,
            int? expires = null,
            string label = null,
            string token = null)
        {
            var myParams = new Dictionary<string, string>();
            myParams.Add("password", password);
            myParams.Add("username", username);
            if (expires != null)
                myParams.Add("expires", expires.Value.ToString(CultureInfo.InvariantCulture));
            if (label != null)
                myParams.Add("label", label);
            if (token != null)
                myParams.Add("token", token);
            GetResponse<object>("user.getapikey", myParams);
        }
    }
}
