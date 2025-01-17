﻿// <auto-generated />
using System;
using AIStudio.Wpf.DataRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AIStudio.Wpf.DataRepository.Migrations
{
    [DbContext(typeof(BaseDbContext))]
    [Migration("20210730070654_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.Base_Action", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Icon");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("Name");

                    b.Property<bool>("NeedAction");

                    b.Property<string>("ParentId");

                    b.Property<int>("Sort");

                    b.Property<string>("TenantId");

                    b.Property<int>("Type");

                    b.Property<string>("Url");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Base_Action");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.Base_AppSecret", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppId");

                    b.Property<string>("AppName");

                    b.Property<string>("AppSecret");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<bool>("Deleted");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("Base_AppSecret");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.Base_Datasource", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<string>("DbLinkId");

                    b.Property<string>("Description");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("Name");

                    b.Property<string>("Sql");

                    b.HasKey("Id");

                    b.ToTable("Base_Datasource");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.Base_DbLink", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConnectionStr");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<string>("DbType");

                    b.Property<bool>("Deleted");

                    b.Property<string>("LinkName");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("Base_DbLink");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.Base_Department", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<bool>("Deleted");

                    b.Property<int>("Level");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("Name");

                    b.Property<string>("ParentId");

                    b.Property<string>("ParentIds");

                    b.Property<string>("ParentNames");

                    b.Property<string>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("Base_Department");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.Base_Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<bool>("Deleted");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("RoleName");

                    b.Property<string>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("Base_Role");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.Base_RoleAction", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActionId");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<bool>("Deleted");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("RoleId");

                    b.Property<string>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("Base_RoleAction");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.Base_User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<DateTime?>("Birthday");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<bool>("Deleted");

                    b.Property<string>("DepartmentId");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("RealName");

                    b.Property<int>("Sex");

                    b.Property<string>("TenantId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Base_User");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.Base_UserLog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<string>("LogContent");

                    b.Property<string>("LogType");

                    b.HasKey("Id");

                    b.ToTable("Base_UserLog");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.Base_UserRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<bool>("Deleted");

                    b.Property<string>("RoleId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Base_UserRole");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.D_Notice", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnyId");

                    b.Property<string>("Appendix");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<bool>("Deleted");

                    b.Property<int>("Mode");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<int>("Status");

                    b.Property<string>("TenantId");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("D_Notice");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.D_NoticeReadingMarks", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<bool>("Deleted");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("NoticeId");

                    b.Property<string>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("D_NoticeReadingMarks");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.D_UserGroup", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Appendix");

                    b.Property<string>("Avatar");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<bool>("Deleted");

                    b.Property<string>("ManagerIds");

                    b.Property<string>("ManagerNames");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("Name");

                    b.Property<string>("Remark");

                    b.Property<string>("TenantId");

                    b.Property<string>("Title");

                    b.Property<int>("Type");

                    b.Property<string>("UserIds");

                    b.Property<string>("UserNames");

                    b.HasKey("Id");

                    b.ToTable("D_UserGroup");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.D_UserMail", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Appendix");

                    b.Property<string>("CCIds");

                    b.Property<string>("CCNames");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<bool>("Deleted");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("ReadingMarks");

                    b.Property<bool>("StarMark");

                    b.Property<int>("Status");

                    b.Property<string>("TenantId");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.Property<int>("Type");

                    b.Property<string>("UserIds");

                    b.Property<string>("UserNames");

                    b.HasKey("Id");

                    b.ToTable("D_UserMail");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.D_UserMessage", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<bool>("Deleted");

                    b.Property<string>("GroupId");

                    b.Property<string>("GroupName");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("ReadingMarks");

                    b.Property<int>("Status");

                    b.Property<string>("TenantId");

                    b.Property<string>("Text");

                    b.Property<int>("Type");

                    b.Property<string>("UserIds");

                    b.Property<string>("UserNames");

                    b.HasKey("Id");

                    b.ToTable("D_UserMessage");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.D_UserMessage_202102", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<bool>("Deleted");

                    b.Property<string>("GroupId");

                    b.Property<string>("GroupName");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("ReadingMarks");

                    b.Property<string>("TenantId");

                    b.Property<string>("Text");

                    b.Property<int>("Type");

                    b.Property<string>("UserIds");

                    b.Property<string>("UserNames");

                    b.HasKey("Id");

                    b.ToTable("D_UserMessage_202102");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.D_UserMessage_202103", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<bool>("Deleted");

                    b.Property<string>("GroupId");

                    b.Property<string>("GroupName");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("ReadingMarks");

                    b.Property<string>("TenantId");

                    b.Property<string>("Text");

                    b.Property<int>("Type");

                    b.Property<string>("UserIds");

                    b.Property<string>("UserNames");

                    b.HasKey("Id");

                    b.ToTable("D_UserMessage_202103");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.D_UserMessage_202104", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<bool>("Deleted");

                    b.Property<string>("GroupId");

                    b.Property<string>("GroupName");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("ReadingMarks");

                    b.Property<string>("TenantId");

                    b.Property<string>("Text");

                    b.Property<int>("Type");

                    b.Property<string>("UserIds");

                    b.Property<string>("UserNames");

                    b.HasKey("Id");

                    b.ToTable("D_UserMessage_202104");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.OA_DefForm", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<bool>("Deleted");

                    b.Property<string>("JSONId");

                    b.Property<int>("JSONVersion");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("Name");

                    b.Property<int>("Sort");

                    b.Property<int>("Status");

                    b.Property<string>("TenantId");

                    b.Property<string>("Text");

                    b.Property<string>("Type");

                    b.Property<string>("Value");

                    b.Property<string>("WorkflowJSON");

                    b.HasKey("Id");

                    b.ToTable("OA_DefForm");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.OA_DefType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<bool>("Deleted");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("Name");

                    b.Property<string>("TenantId");

                    b.Property<string>("Type");

                    b.Property<string>("Unit");

                    b.HasKey("Id");

                    b.ToTable("OA_DefType");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.OA_UserForm", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlreadyUserIds");

                    b.Property<string>("AlreadyUserNames");

                    b.Property<string>("Appendix");

                    b.Property<string>("ApplicantDepartment");

                    b.Property<string>("ApplicantDepartmentId");

                    b.Property<string>("ApplicantRole");

                    b.Property<string>("ApplicantRoleId");

                    b.Property<string>("ApplicantUser");

                    b.Property<string>("ApplicantUserId");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<string>("CurrentNode");

                    b.Property<string>("DefFormId");

                    b.Property<string>("DefFormJsonId");

                    b.Property<int>("DefFormJsonVersion");

                    b.Property<string>("DefFormName");

                    b.Property<bool>("Deleted");

                    b.Property<DateTime?>("ExpectedDate");

                    b.Property<string>("ExtendJSON");

                    b.Property<double>("Flag");

                    b.Property<int>("Grade");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("Remarks");

                    b.Property<int>("Status");

                    b.Property<string>("SubType");

                    b.Property<string>("TenantId");

                    b.Property<string>("Text");

                    b.Property<string>("Type");

                    b.Property<string>("Unit");

                    b.Property<string>("UserIds");

                    b.Property<string>("UserNames");

                    b.Property<string>("UserRoleIds");

                    b.Property<string>("UserRoleNames");

                    b.HasKey("Id");

                    b.ToTable("OA_UserForm");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.OA_UserFormStep", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<bool>("Deleted");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("Remarks");

                    b.Property<string>("RoleIds");

                    b.Property<string>("RoleNames");

                    b.Property<int>("Status");

                    b.Property<string>("StepName");

                    b.Property<string>("TenantId");

                    b.Property<string>("UserFormId");

                    b.HasKey("Id");

                    b.ToTable("OA_UserFormStep");
                });

            modelBuilder.Entity("AIStudio.Wpf.Entity.Models.Quartz_Task", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApiUrl");

                    b.Property<string>("AuthKey");

                    b.Property<string>("AuthValue");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Describe");

                    b.Property<bool>("ForbidEdit");

                    b.Property<bool>("ForbidOperate");

                    b.Property<string>("GroupName");

                    b.Property<string>("Interval");

                    b.Property<DateTime?>("LastRunTime");

                    b.Property<string>("ModifyId");

                    b.Property<string>("ModifyName");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("RequestType");

                    b.Property<int>("Status");

                    b.Property<string>("TaskName");

                    b.Property<string>("TenantId");

                    b.HasKey("Id");

                    b.ToTable("Quartz_Task");
                });
#pragma warning restore 612, 618
        }
    }
}
