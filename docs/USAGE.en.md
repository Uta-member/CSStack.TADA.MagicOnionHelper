# CSStack.TADA.MagicOnionHelper ? Detailed Usage (English)

This document shows how to define DTOs, services, and wire up MagicOnion server/client using this helper library.

Prerequisites
- .NET 8
- MagicOnion
- MessagePack

1. Define UseCase contracts and DTOs
- ICommandServiceDTO/IQueryServiceDTO: marker interfaces for UseCase layer DTOs
- ICommandService/IQueryService: application services executed by server bases

Example (UseCase layer)
- Request/Response DTOs implement ICommandServiceDTO/IQueryServiceDTO
- Services expose ExecuteAsync methods

2. Define MessagePack transport types (IMPDTO)
- Implement IMPDTO<TDTO, TSelf>
- Provide conversions: static TSelf FromDTO(TDTO dto) and TDTO ToDTO()
- Decorate with MessagePackObject/Key attributes

3. Define MagicOnion service interfaces
- Use IMOCommandService or IMOQueryService
- The generic parameters pair the MessagePack types with UseCase DTOs

4. Implement server services with bases
- Derive from MOCommandServiceBase/MOQueryServiceBase
- Implement Execute to call ExecuteCore
- The base handles CancellationToken and DTO conversion

5. Implement client services with bases
- Derive from MOCommandServiceClientBase/MOQueryServiceClientBase
- Inject IMOClientChannelFactory
- Call ExecuteAsync; conversions are handled for you

6. Optional values
- Use MPOptional<T> to serialize Optionals
- Map via MPOptional.FromOptional / ToOptional, or OptionalExtensions

Code Sketches
- Command (no response)
  - UseCase: class CreateUserRequest : ICommandServiceDTO; interface ICommandService<CreateUserRequest>
  - MP type: [MessagePackObject] record MPCreateUserRequest : IMPDTO<CreateUserRequest, MPCreateUserRequest>
  - MagicOnion interface: interface ICreateUserService : IMOCommandService<ICreateUserService, MPCreateUserRequest, CreateUserRequest>
  - Server: class CreateUserService : MOCommandServiceBase<...> { public override UnaryResult Execute(MPCreateUserRequest req) => ExecuteCore(req); }
  - Client: class CreateUserClient : MOCommandServiceClientBase<...>

- Query (with response)
  - UseCase: class GetUserRequest : IQueryServiceDTO; class GetUserResponse : IQueryServiceDTO; interface IQueryService<GetUserRequest, GetUserResponse>
  - MP types: MPG etUserRequest/Response implementing IMPDTO
  - MagicOnion interface: interface IGetUserService : IMOQueryService<IGetUserService, MPGetUserRequest, GetUserRequest, MPGetUserResponse, GetUserResponse>
  - Server: class GetUserService : MOQueryServiceBase<...> { public override UnaryResult<MPGetUserResponse> Execute(MPGetUserRequest req) => ExecuteCore(req); }
  - Client: class GetUserClient : MOQueryServiceClientBase<...>

Channel factory
- Provide an implementation of IMOClientChannelFactory (MOClientChannelFactory or your own) to supply GrpcChannel
- Example: new MOClientChannelFactory(GrpcChannel.ForAddress("https://localhost:5001"))

Notes
- Obsolete APIs (WithRes/WithoutReq) are kept for backward compatibility but will be removed
- Cancellation flows from MagicOnion CallContext to UseCase services
- Ensure your MessagePack types and DTOs are versioned carefully (Keys and schema)
