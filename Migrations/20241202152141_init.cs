using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComponentKinds",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuComponentKind = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    EnComponentKind = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentKinds", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ComponentTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuComponentType = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    EnComponentType = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuTechnologyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnTechnologyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Capacitors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocID = table.Column<long>(type: "bigint", nullable: true),
                    ComponentName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Type_ID = table.Column<int>(type: "int", nullable: true),
                    Kind_ID = table.Column<int>(type: "int", nullable: true),
                    ManufacturerName_ID = table.Column<int>(type: "int", nullable: true),
                    OutputType = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    MinVoltage = table.Column<double>(type: "float", nullable: true),
                    MaxVoltage = table.Column<double>(type: "float", nullable: true),
                    MaxCapacity = table.Column<double>(type: "float", nullable: true),
                    MinCapacity = table.Column<double>(type: "float", nullable: true),
                    MinOperatingTemperature = table.Column<double>(type: "float", nullable: true),
                    MaxOperatingTemperature = table.Column<double>(type: "float", nullable: true),
                    AcceptableCapacityIncrease = table.Column<double>(type: "float", nullable: true),
                    AcceptableСapacityReduction = table.Column<double>(type: "float", nullable: true),
                    QualicationSG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualicationЕС = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capacitors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Capacitors_ComponentKinds_Kind_ID",
                        column: x => x.Kind_ID,
                        principalTable: "ComponentKinds",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Capacitors_ComponentTypes_Type_ID",
                        column: x => x.Type_ID,
                        principalTable: "ComponentTypes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Capacitors_Manufacturers_ManufacturerName_ID",
                        column: x => x.ManufacturerName_ID,
                        principalTable: "Manufacturers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Diods",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocID = table.Column<long>(type: "bigint", nullable: true),
                    ComponentName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Type_ID = table.Column<int>(type: "int", nullable: true),
                    Kind_ID = table.Column<int>(type: "int", nullable: true),
                    ManufacturerName_ID = table.Column<int>(type: "int", nullable: true),
                    MaxPermissibleDCVoltage = table.Column<double>(type: "float", nullable: true),
                    MinOperatingTemperature = table.Column<double>(type: "float", nullable: true),
                    MaxOperatingTemperature = table.Column<double>(type: "float", nullable: true),
                    MaxPermissibleAverageDirectCurrent = table.Column<double>(type: "float", nullable: true),
                    MaxiPermissibleDirectCurrent = table.Column<double>(type: "float", nullable: true),
                    RadiationResistance = table.Column<double>(type: "float", nullable: true),
                    RadiationResistanceI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualicationSG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualicationЕС = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Package = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diods", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Diods_ComponentKinds_Kind_ID",
                        column: x => x.Kind_ID,
                        principalTable: "ComponentKinds",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Diods_ComponentTypes_Type_ID",
                        column: x => x.Type_ID,
                        principalTable: "ComponentTypes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Diods_Manufacturers_ManufacturerName_ID",
                        column: x => x.ManufacturerName_ID,
                        principalTable: "Manufacturers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Resistors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocID = table.Column<long>(type: "bigint", nullable: true),
                    ComponentName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Type_ID = table.Column<int>(type: "int", nullable: true),
                    Kind_ID = table.Column<int>(type: "int", nullable: true),
                    ManufacturerName_ID = table.Column<int>(type: "int", nullable: true),
                    PowerRating = table.Column<double>(type: "float", nullable: true),
                    MinVoltage = table.Column<double>(type: "float", nullable: true),
                    MaxVoltage = table.Column<double>(type: "float", nullable: true),
                    MinRatedResistance = table.Column<double>(type: "float", nullable: true),
                    MaxRatedResistance = table.Column<double>(type: "float", nullable: true),
                    ResistanceTolerance = table.Column<double>(type: "float", nullable: true),
                    MinOperatingTemperature = table.Column<double>(type: "float", nullable: true),
                    MaxOperatingTemperature = table.Column<double>(type: "float", nullable: true),
                    CurrentLimit = table.Column<double>(type: "float", nullable: true),
                    QualicationSG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualicationЕС = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Package = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resistors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Resistors_ComponentKinds_Kind_ID",
                        column: x => x.Kind_ID,
                        principalTable: "ComponentKinds",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Resistors_ComponentTypes_Type_ID",
                        column: x => x.Type_ID,
                        principalTable: "ComponentTypes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Resistors_Manufacturers_ManufacturerName_ID",
                        column: x => x.ManufacturerName_ID,
                        principalTable: "Manufacturers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Transistors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocID = table.Column<long>(type: "bigint", nullable: true),
                    ComponentName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Type_ID = table.Column<int>(type: "int", nullable: true),
                    Kind_ID = table.Column<int>(type: "int", nullable: true),
                    ManufacturerName_ID = table.Column<int>(type: "int", nullable: true),
                    MaxPermissibleDCVoltage = table.Column<double>(type: "float", nullable: true),
                    MinOperatingTemperature = table.Column<double>(type: "float", nullable: true),
                    MaxOperatingTemperature = table.Column<double>(type: "float", nullable: true),
                    MaxPermissibleDCCollectorCurrent = table.Column<double>(type: "float", nullable: true),
                    RadiationResistance = table.Column<double>(type: "float", nullable: true),
                    RadiationResistanceI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualicationSG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QualicationЕС = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Package = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transistors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Transistors_ComponentKinds_Kind_ID",
                        column: x => x.Kind_ID,
                        principalTable: "ComponentKinds",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Transistors_ComponentTypes_Type_ID",
                        column: x => x.Type_ID,
                        principalTable: "ComponentTypes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Transistors_Manufacturers_ManufacturerName_ID",
                        column: x => x.ManufacturerName_ID,
                        principalTable: "Manufacturers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Microchips",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocID = table.Column<long>(type: "bigint", nullable: true),
                    ComponentName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Type_ID = table.Column<int>(type: "int", nullable: false),
                    Kind_ID = table.Column<int>(type: "int", nullable: false),
                    ManufacturerName_ID = table.Column<int>(type: "int", nullable: false),
                    Interfaces = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinVoltage = table.Column<double>(type: "float", nullable: false),
                    MaxVoltage = table.Column<double>(type: "float", nullable: false),
                    Frequency = table.Column<double>(type: "float", nullable: true),
                    BitDepthValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsumptionCurrent = table.Column<double>(type: "float", nullable: true),
                    TechnologyName_ID = table.Column<int>(type: "int", nullable: false),
                    MinOperatingTemperature = table.Column<double>(type: "float", nullable: false),
                    MaxOperatingTemperature = table.Column<double>(type: "float", nullable: false),
                    RadiationResistance = table.Column<double>(type: "float", nullable: true),
                    RadiationResistanceI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemoryFormat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SamplingTime = table.Column<double>(type: "float", nullable: true),
                    Qualication = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark1 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Microchips", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Microchips_ComponentKinds_Kind_ID",
                        column: x => x.Kind_ID,
                        principalTable: "ComponentKinds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Microchips_ComponentTypes_Type_ID",
                        column: x => x.Type_ID,
                        principalTable: "ComponentTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Microchips_Manufacturers_ManufacturerName_ID",
                        column: x => x.ManufacturerName_ID,
                        principalTable: "Manufacturers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Microchips_Technologies_TechnologyName_ID",
                        column: x => x.TechnologyName_ID,
                        principalTable: "Technologies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Capacitors_Kind_ID",
                table: "Capacitors",
                column: "Kind_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Capacitors_ManufacturerName_ID",
                table: "Capacitors",
                column: "ManufacturerName_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Capacitors_Type_ID",
                table: "Capacitors",
                column: "Type_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Diods_Kind_ID",
                table: "Diods",
                column: "Kind_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Diods_ManufacturerName_ID",
                table: "Diods",
                column: "ManufacturerName_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Diods_Type_ID",
                table: "Diods",
                column: "Type_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Microchips_Kind_ID",
                table: "Microchips",
                column: "Kind_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Microchips_ManufacturerName_ID",
                table: "Microchips",
                column: "ManufacturerName_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Microchips_TechnologyName_ID",
                table: "Microchips",
                column: "TechnologyName_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Microchips_Type_ID",
                table: "Microchips",
                column: "Type_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Resistors_Kind_ID",
                table: "Resistors",
                column: "Kind_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Resistors_ManufacturerName_ID",
                table: "Resistors",
                column: "ManufacturerName_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Resistors_Type_ID",
                table: "Resistors",
                column: "Type_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Transistors_Kind_ID",
                table: "Transistors",
                column: "Kind_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Transistors_ManufacturerName_ID",
                table: "Transistors",
                column: "ManufacturerName_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Transistors_Type_ID",
                table: "Transistors",
                column: "Type_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Capacitors");

            migrationBuilder.DropTable(
                name: "Diods");

            migrationBuilder.DropTable(
                name: "Microchips");

            migrationBuilder.DropTable(
                name: "Resistors");

            migrationBuilder.DropTable(
                name: "Transistors");

            migrationBuilder.DropTable(
                name: "Technologies");

            migrationBuilder.DropTable(
                name: "ComponentKinds");

            migrationBuilder.DropTable(
                name: "ComponentTypes");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
