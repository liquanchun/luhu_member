﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace MvcMember.Models
{
    public class WebMemberDBContext :DbContext
    {
        //去掉自动表名复数
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<AdminUser> AdminUserDb { get; set; }
        public DbSet<Branch> BranchDb { get; set; }
        public DbSet<CardInfo> CardInfoDb { get; set; }
        public DbSet<CardType> CardTypeDb { get; set; }
        public DbSet<Consume> ConsumeDb { get; set; }

        public DbSet<Employee> EmployeeDb { get; set; }
        public DbSet<GuestBook> GuestBookDb { get; set; }
        public DbSet<ItemList> ItemListDb { get; set; }
        public DbSet<Member> MemberDb { get; set; }
        public DbSet<Member2> Member2Db { get; set; }

        public DbSet<Recharge> RechargeDb { get; set; }
        public DbSet<SysLog> SysLogDb { get; set; }
        public DbSet<SysSetting> SysSettingDb { get; set; }
        public DbSet<V_CardInfo> V_CardInfoDb { get; set; }
        public DbSet<V_Consume> V_ConsumeDb { get; set; }

        public DbSet<V_Member> V_MemberDb { get; set; }
        public DbSet<V_MemberUpdate> V_MemberUpdateDb { get; set; }
        public DbSet<V_Recharge> V_RechargeDb { get; set; }

        public DbSet<V_R_CardInfo> V_R_CardInfoDb { get; set; }
        public DbSet<ServiceInte> ServiceInteDb { get; set; }
        public DbSet<Activity> ActivityDb { get; set; }
        public DbSet<ConsumeItem> ConsumeItemDb { get; set; }
        public DbSet<PayList> PayListDb { get; set; }
        public DbSet<MemberUpdate> MemberUpdateDb { get; set; }

        public DbSet<AdjustInte> AdjustInteDb { get; set; }
        public DbSet<SysFun> SysFunDb { get; set; }
        public DbSet<UserAuth> UserAuthDb { get; set; }
        public DbSet<V_SysFunRole> V_SysFunRole { get; set; }
        public DbSet<ServiceItem> ServiceItemDb { get; set; }
        public DbSet<ServiceItem2> ServiceItem2Db { get; set; }
        public DbSet<CardServiceItem> CardServiceItemDb { get; set; }
        public DbSet<V_CardServiceItem> V_CardServiceItemDb { get; set; }
        public DbSet<Smssend> SmssendDb { get; set; }
        public DbSet<Complain> ComplainDb { get; set; }

        public DbSet<V_CallBack> V_CallBackDb { get; set; }
        public DbSet<GiftInfo> GiftInfoDb { get; set; }
        public DbSet<V_ExchangePoint> V_ExchangePointDb { get; set; }

        public DbSet<V_AdminUser> V_AdminUserDb { get; set; }
    }
}