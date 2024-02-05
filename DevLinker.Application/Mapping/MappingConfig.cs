using AutoMapper;
using DevLinker.Application.UseCases.Account.Commands.RegisterUser;
using DevLinker.Application.UseCases.Issues.Commands.CreateIssue;
using DevLinker.Application.UseCases.Issues.Commands.UpdateIssue;
using DevLinker.Application.UseCases.IssuesMembers.Commands.AddIssueMember;
using DevLinker.Application.UseCases.Workspaces.Commands.CreateWorkspace;
using DevLinker.Application.UseCases.Workspaces.Commands.UpdateWorkspace;
using DevLinker.Application.UseCases.WorkspacesMembers.Commands.AddWorkspaceMember;
using DevLinker.Domain.Dto;
using DevLinker.Domain.Entities;

namespace DevLinker.Application.Mapping
{
	public class MappingsConfig : Profile
	{
		public MappingsConfig()
		{
			/*
			 * Workspace
			 */
			CreateMap<CreateWorkspaceCommand, Workspace>();
			CreateMap<UpdateWorkspaceCommand, Workspace>();
			CreateMap<Workspace, UserWorkspacesDto>();

			/*
			 * User
			 */
			CreateMap<RegisterUserCommand, AppUser>();
			CreateMap<AppUser, UserInfoDto>()
				.ForMember(x => x.Claims, opt => opt.Ignore());

			/*
			 * WorkspaceMember
			 */
			CreateMap<AddWorkspaceMemberCommand, WorkspaceMember>();

			/*
			 * Issue
			 */
			CreateMap<CreateIssueCommand, Issue>();
			CreateMap<UpdateIssueCommand, Issue>();

			/*
			 * IssueMember
			 */
			CreateMap<AddIssueMemberCommand, IssueMember>()
				.ForSourceMember(x => x.WorkspaceId, opt => opt.DoNotValidate());
		}
	}
}
