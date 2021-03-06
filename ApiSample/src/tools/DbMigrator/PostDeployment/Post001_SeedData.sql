INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'366d6cdd-7391-45ac-8e29-1f84afbc4b8f', N'Moderator', NULL, N'f5a05fed-0c43-42ba-9f06-be85bb85fdab')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'8dd1df58-375b-4277-8284-aeae1e49f0a8', N'Administrator', NULL, N'f444c1b6-a009-4092-b6a6-92e40f17d8a3')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'b421e928-0613-9ebd-a64c-f10b6a706e73', N'User', NULL, N'b18e0611-ee41-46f3-bc84-f604f9eac598')
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'22e40406-8a9d-2d82-912c-5d6a640ee696', NULL, NULL, N'user', N'USER', N'user@example.com', N'USER@EXAMPLE.COM', 1, N'AQAAAAEAACcQAAAAEIJr2q1j2DkPZjg+c8WLHzL9K4dYma5ly3YJX4dGjFCdtLYu29qPTNZ9P+s1ZOIeaQ==', N'63ec02f7-532a-4a39-a6f7-3fdb3db36cb8', N'01939c2b-1c16-4a31-b981-11199324e947', NULL, 0, 0, NULL, 0, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'7db4adec-3cd2-4c40-80b1-13e791060185', NULL, NULL, N'moderator', N'MODERATOR', N'moderator@example.com', N'MODERATOR@EXAMPLE.COM', 1, N'AQAAAAEAACcQAAAAEAmQVs85NKpZzhkzfm7JnJO3b6wj4DBsda301Wyj77miBBRYnNZBawWcyyOvTZgKeg==', N'20785cd5-12b6-46a1-9d73-e56ae8320268', N'c72d2780-1c30-4113-919d-b556cab5bb45', NULL, 0, 0, NULL, 0, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'ed19cc4d-a03b-467a-b714-5fe9e309e605', NULL, NULL, N'admin', N'ADMIN', N'admin@example.com', N'ADMIN@EXAMPLE.COM', 1, N'AQAAAAEAACcQAAAAEPDkZd0Bw/43her5TWFeycAElPBElSJg7OKhBESdvrbDjI+XuPD2Hqx/7+ZhSCyJUA==', N'a2b31c9f-83e9-4930-ac65-d05d14ff62df', N'dda83d60-3a9e-43a8-a947-df624eb6fd48', NULL, 0, 0, NULL, 0, 0)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7db4adec-3cd2-4c40-80b1-13e791060185', N'366d6cdd-7391-45ac-8e29-1f84afbc4b8f')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ed19cc4d-a03b-467a-b714-5fe9e309e605', N'8dd1df58-375b-4277-8284-aeae1e49f0a8')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'22e40406-8a9d-2d82-912c-5d6a640ee696', N'b421e928-0613-9ebd-a64c-f10b6a706e73')
GO
SET IDENTITY_INSERT [dbo].[Company] ON 
GO
INSERT [dbo].[Company] ([CompanyId], [Name], [Description], [Code]) VALUES (1, N'South Hook Gas', N'South Hook Gas', N'SHG')
GO
INSERT [dbo].[Company] ([CompanyId], [Name], [Description], [Code]) VALUES (2, N'Quatar Gas', N'Quatar Gas', N'QG')
GO
INSERT [dbo].[Company] ([CompanyId], [Name], [Description], [Code]) VALUES (3, N'A2A S.p.A.', N'A2A', N'A2A')
GO
INSERT [dbo].[Company] ([CompanyId], [Name], [Description], [Code]) VALUES (4, N'Abu Dhabi Gas Liquefaction Company Limited', N'ADGAS', N'ADGAS')
GO
INSERT [dbo].[Company] ([CompanyId], [Name], [Description], [Code]) VALUES (5, N'Alpiq AG', N'Alpiq AG', N'Alpiq AG')
GO
INSERT [dbo].[Company] ([CompanyId], [Name], [Description], [Code]) VALUES (6, N'Angola LNG Limited', N'ALNG', N'ALNG')
GO
INSERT [dbo].[Company] ([CompanyId], [Name], [Description], [Code]) VALUES (7, N'Axpo Solutions AG (Axpo Trading AG; Axpo Trading Ltd.)', N'Axpo Solutions', N'Axpo Solutions')
GO
INSERT [dbo].[Company] ([CompanyId], [Name], [Description], [Code]) VALUES (8, N'SHELL GLOBAL LNG LIMITED (SINGAPORE BRANCH)', N'SHELL GLOBAL LNG LIMITED SG', N'SHELL GLOBAL LNG LIMITED SG')
GO
INSERT [dbo].[Company] ([CompanyId], [Name], [Description], [Code]) VALUES (9, N'Bharat Petroleum Corporation Ltd', N'Bharat Petroleum Corporation', N'Bharat Petroleum Corporation')
GO
SET IDENTITY_INSERT [dbo].[Company] OFF
GO
SET IDENTITY_INSERT [dbo].[Location] ON 
GO
INSERT [dbo].[Location] ([LocationId], [Name], [Description]) VALUES (1, N'South Hook', N'South Hook')
GO
INSERT [dbo].[Location] ([LocationId], [Name], [Description]) VALUES (2, N'Portland Bight FSRU', N'FSU in Jamaica Golar Arctic')
GO
INSERT [dbo].[Location] ([LocationId], [Name], [Description]) VALUES (3, N'Port Qasim', N'Pakistan LNG terminal')
GO
INSERT [dbo].[Location] ([LocationId], [Name], [Description]) VALUES (4, N'Sabine Pass', N'Sabine Pass LNG')
GO
INSERT [dbo].[Location] ([LocationId], [Name], [Description]) VALUES (5, N'Soyo Angola', N'Soyo LNG Terminal')
GO
INSERT [dbo].[Location] ([LocationId], [Name], [Description]) VALUES (6, N'Dahej', N'Dahej')
GO
INSERT [dbo].[Location] ([LocationId], [Name], [Description]) VALUES (7, N'ETKI FSRU Turkey', N'ETKI FSRU Turkey')
GO
INSERT [dbo].[Location] ([LocationId], [Name], [Description]) VALUES (8, N'Oita', N'Oita LNG Terminal')
GO
INSERT [dbo].[Location] ([LocationId], [Name], [Description]) VALUES (9, N'Ain Sokhna FSRU', N'Al-Sokhna Port Egypt')
GO
SET IDENTITY_INSERT [dbo].[Location] OFF
GO
SET IDENTITY_INSERT [dbo].[ToDoItem] ON 
GO
INSERT [dbo].[ToDoItem] ([ToDoItemId], [Title], [Description], [Email], [IsDone]) VALUES (8, N'Title673ebb47-c70e-4836-ab04-53f7bf33c910', N'Updated', N'ctorres9p@economist.com', 1)
GO
SET IDENTITY_INSERT [dbo].[ToDoItem] OFF
GO
