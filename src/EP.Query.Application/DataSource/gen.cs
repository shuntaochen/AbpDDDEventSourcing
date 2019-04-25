
using System.Threading.Tasks;

namespace TempService
{
    public interface ITempService
    {

        Task<CreateFolderOutput> CreateFolder(CreateFolderInput input);


        Task<GetALlOutput> GetALl(GetALlInput input);


        Task<CreateOutput> Create(CreateInput input);


        Task<RenameOutput> Rename(RenameInput input);


        Task<SaveOutput> Save(SaveInput input);


        Task<DeleteOutput> Delete(DeleteInput input);


        Task<GetSchemaOutput> GetSchema(GetSchemaInput input);


        Task<SaveConfigOutput> SaveConfig(SaveConfigInput input);

    }



    public class CreateFolderInput
    {
    }

    public class CreateFolderOutput
    {
    }


    public class GetALlInput
    {
    }

    public class GetALlOutput
    {
    }


    public class CreateInput
    {
    }

    public class CreateOutput
    {
    }


    public class RenameInput
    {
    }

    public class RenameOutput
    {
    }


    public class SaveInput
    {
    }

    public class SaveOutput
    {
    }


    public class DeleteInput
    {
    }

    public class DeleteOutput
    {
    }


    public class GetSchemaInput
    {
    }

    public class GetSchemaOutput
    {
    }


    public class SaveConfigInput
    {
    }

    public class SaveConfigOutput
    {
    }


}