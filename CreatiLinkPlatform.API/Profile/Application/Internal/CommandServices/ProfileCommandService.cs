using CreatiLinkPlatform.API.Profile.Domain.Model.Aggregates;
using CreatiLinkPlatform.API.Profile.Domain.Model.Commands;
using CreatiLinkPlatform.API.Profile.Domain.Repositories;
using CreatiLinkPlatform.API.Profile.Domain.Services;
using CreatiLinkPlatform.API.Shared.Domain.Repositories;
using SocialLinks = CreatiLinkPlatform.API.Profile.Domain.Model.Aggregates.SocialLinks;
using CreatiLinkPlatform.API.IAM.Domain.Repositories; // ← importá esto


namespace CreatiLinkPlatform.API.Profile.Application.Internal.CommandServices;


public class ProfileCommandService(
    IProfileRepository profileRepository,
    IUserRepository userRepository, 
    IUnitOfWork unitOfWork)
    : IProfileCommandService
{
    
    public async Task<Domain.Model.Aggregates.Profile?> Handle(CreateProfileCommand command)
    {
        var user = await userRepository.FindByIdAsync(command.UserId);
        if (user == null) return null;

     
        var existingProfile = await profileRepository.FindByUserIdAsync(command.UserId);
        if (existingProfile != null)
        {
           
            return null;
        }

        var socialLinks = new SocialLinks(
            command.Social.Facebook,
            command.Social.X,
            command.Social.Instagram
        );

        var profile = new Domain.Model.Aggregates.Profile(
            id: 0, 
            userId: command.UserId,
            name: command.Name,
            location: command.Location,
            bio: command.Bio,
            image: command.Image,
            icon: command.Icon,
            experience: command.Experience,
            social: socialLinks
        );

        await profileRepository.AddAsync(profile);
        await unitOfWork.CompleteAsync();
        return profile;
    }


    public async Task<Domain.Model.Aggregates.Profile?> Handle(UpdateProfileCommand command)
    {
        var socialLinks = new SocialLinks(
            command.Social.Facebook,
            command.Social.X,
            command.Social.Instagram
 
        );
        var profile = await profileRepository.FindByIdAsync(command.Id);
        if (profile == null)
            return null;

        profile.UpdateProfile(
            command.Name,
            command.Location,
            command.Bio,
            command.Image,
            command.Icon,
            command.Experience,
            socialLinks

        );

        profileRepository.Update(profile);
        await unitOfWork.CompleteAsync();
        return profile;
    }

    public async Task<bool> Handle(DeleteProfileCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.Id);
        if (profile == null)
            return false;

        profileRepository.Remove(profile);
        await unitOfWork.CompleteAsync();
        return true;
    }
    
}

