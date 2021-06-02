// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicDto.cs" company="Gateway">
//   Gateway 2020
// </copyright>
// <summary>
//   Defines the BasicDto type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gateway.Common.Dto
{
    using Abp.Application.Services.Dto;
    using Abp.Localization;

    using Gateway.StaticNames;

    /// <summary>
    ///     The basic DTO.
    /// </summary>
    public class BasicDto : EntityDto
    {
        /// <summary>
        ///     The name.
        /// </summary>
        private string name;

        /// <summary>
        ///     Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name
        {
            get => this.name;
            set
            {
                var translated = LocalizationHelper.GetString(LocalizationSourceStaticNames.SourceName, value);
                if (translated.StartsWith('['))
                {
                    translated = value;
                }

                this.DisplayName = translated;
                this.name = value;
            }
        }
    }
}
