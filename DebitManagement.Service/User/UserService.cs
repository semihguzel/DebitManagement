using System.Net;
using AutoMapper;
using DebitManagement.Base;
using DebitManagement.Core.Interfaces;

namespace DebitManagement.Service.User;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Core.Entities.User> CheckUserAndReturn(string username)
    {
        var user = await _userRepository.GetByUsername(username);

        if (user == null)
            throw new HttpException(HttpStatusCode.NotAcceptable,
                "Couldn't find user. Please check sent data and try again");

        return user;
    }

    public async Task<Core.Entities.User> CheckByIdAndReturn(Guid id)
    {
        var product = await _userRepository.GetByIdAsync(id);

        if (product == null)
            throw new HttpException(HttpStatusCode.NotAcceptable, "User does not exists. Please check sent data");

        return product;
    }
}