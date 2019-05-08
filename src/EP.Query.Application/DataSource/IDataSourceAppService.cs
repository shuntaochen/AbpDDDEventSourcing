
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Query.DataSource
{
    public interface IDataSourceAppService
    {

        Task<CreateFolderOutput> CreateFolder(CreateFolderInput input);


        Task<GetALlOutput> GetALl(GetALlInput input);


        Task<DataSourceDto> Get(int id);


        Task<RenameOutput> Rename(RenameInput input);


        Task<SaveOutput> Save(SaveInput input);


        Task Delete(DeleteInput input);


        Task<GetSchemasOutput> GetSchemas();

        Task<Dictionary<string, string>> GetQueryColumns(GetQueryColumnsInput input);

        Task<GetQueryDataOutput> GetQueryData(GetQueryDataInput input);


    }



    public class CreateFolderInput
    {
        [Required]
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }

    public class CreateFolderOutput
    {
        public int Id { get; set; }
    }


    public class GetALlInput : PagedResultRequestDto
    {
        public int FolderId { get; set; }
    }

    public class GetALlOutput : PagedResultDto<DataSource>
    {
        public List<DataSourceFolderDto> Folders { get; set; } = new List<DataSourceFolderDto>();
    }



    public class RenameInput
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public class RenameOutput
    {
        public int Id { get; set; }
    }


    public class SaveInput
    {
        public DataSourceDto DataSource { get; set; } = new DataSourceDto();


    }

    public class SaveOutput
    {
        public int Id { get; set; }
    }


    public class DeleteInput
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }



    public class GetSchemasInput
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }

    public class GetSchemasOutput
    {
        public JArray FieldInfos { get; set; } = new JArray();
    }


    public class SaveConfigInput
    {
        public DataSourceDto DataSource { get; set; }
    }

    public class SaveConfigOutput
    {
        public int Id { get; set; }
    }


    public class GetQueryDataInput : PagedResultRequestDto
    {
        [Required]
        public string TableName { get; set; }
        public List<string> AndConditions { get; set; } = new List<string>();

    }
    public class GetQueryDataOutput : PagedResultDto<JObject>
    {

    }
    public class GetQueryColumnsInput
    {
        [Required]
        public string TableName { get; set; }
        public List<string> AndConditions { get; set; } = new List<string>();
    }

    public class GetQueryColumnsOutput
    {

    }

}