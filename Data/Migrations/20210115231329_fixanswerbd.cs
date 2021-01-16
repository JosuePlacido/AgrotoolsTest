using Microsoft.EntityFrameworkCore.Migrations;

namespace Agrotools.Data.Migrations
{
    public partial class fixanswerbd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_answer_tb_question_QuestionId",
                table: "tb_answer");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_answer_tb_question_QuestionId",
                table: "tb_answer",
                column: "QuestionId",
                principalTable: "tb_question",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_answer_tb_question_QuestionId",
                table: "tb_answer");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_answer_tb_question_QuestionId",
                table: "tb_answer",
                column: "QuestionId",
                principalTable: "tb_question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
