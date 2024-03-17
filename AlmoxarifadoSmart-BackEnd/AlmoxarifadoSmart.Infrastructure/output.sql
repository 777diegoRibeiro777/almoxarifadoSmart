IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [CategoriaMotivo] (
    [id] int NOT NULL IDENTITY,
    [nome] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_CategoriaMotivo] PRIMARY KEY ([id])
);
GO

CREATE TABLE [Departamento] (
    [id] int NOT NULL IDENTITY,
    [nome] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Departamento] PRIMARY KEY ([id])
);
GO

CREATE TABLE [Funcionario] (
    [id] int NOT NULL IDENTITY,
    [nome] nvarchar(255) NOT NULL,
    [cargo] nvarchar(255) NOT NULL,
    [id_departamento] int NOT NULL,
    CONSTRAINT [PK_Funcionario] PRIMARY KEY ([id])
);
GO

CREATE TABLE [LOGROBO] (
    [iDlOG] int NOT NULL IDENTITY,
    [CodigoRobo] nvarchar(max) NOT NULL,
    [UsuarioRobo] nvarchar(max) NOT NULL,
    [DateLog] datetime2 NOT NULL,
    [Etapa] nvarchar(max) NOT NULL,
    [InformacaoLog] nvarchar(max) NOT NULL,
    [IdProdutoAPI] int NOT NULL,
    CONSTRAINT [PK_LOGROBO] PRIMARY KEY ([iDlOG])
);
GO

CREATE TABLE [Produto] (
    [id] int NOT NULL IDENTITY,
    [descricao] nvarchar(255) NOT NULL,
    [preco] decimal(18,0) NOT NULL,
    [estoque_atual] int NULL DEFAULT (((0))),
    [estoque_minimo] int NOT NULL,
    [BranchmarkingId] int NULL,
    [ProdutoScraperModelId] int NULL,
    CONSTRAINT [PK_Produto] PRIMARY KEY ([id])
);
GO

CREATE TABLE [Motivo] (
    [id] int NOT NULL IDENTITY,
    [nome] nvarchar(255) NOT NULL,
    [id_categoriamotivo] int NOT NULL,
    CONSTRAINT [PK_Motivo] PRIMARY KEY ([id]),
    CONSTRAINT [FK__Motivo__id_categ__45F365D3] FOREIGN KEY ([id_categoriamotivo]) REFERENCES [CategoriaMotivo] ([id])
);
GO

CREATE TABLE [Requisicao] (
    [id] int NOT NULL IDENTITY,
    [prioridade] nvarchar(255) NOT NULL,
    [id_departamento] int NOT NULL,
    [id_funcionario] int NOT NULL,
    [created_at] datetime NULL DEFAULT ((getdate())),
    CONSTRAINT [PK_Requisicao] PRIMARY KEY ([id]),
    CONSTRAINT [FK__Requisica__id_de__440B1D61] FOREIGN KEY ([id_departamento]) REFERENCES [Departamento] ([id]),
    CONSTRAINT [FK__Requisica__id_fu__44FF419A] FOREIGN KEY ([id_funcionario]) REFERENCES [Funcionario] ([id])
);
GO

CREATE TABLE [Benchmarkings] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Loja] int NOT NULL,
    [Economia] decimal(18,2) NOT NULL,
    [Link] nvarchar(max) NOT NULL,
    [Create_at] datetime2 NOT NULL,
    [IdProduto] int NOT NULL,
    CONSTRAINT [PK_Benchmarkings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Produto_Benchmarking] FOREIGN KEY ([IdProduto]) REFERENCES [Produto] ([id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProdutoScraper] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Loja] Int NOT NULL,
    [IdProduto] int NOT NULL,
    CONSTRAINT [PK_ProdutoScraper] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProdutoScraper_Produto] FOREIGN KEY ([IdProduto]) REFERENCES [Produto] ([id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProdutoRequisicao] (
    [id_produto] int NOT NULL,
    [id_requisicao] int NOT NULL,
    CONSTRAINT [PK__ProdutoR__9781356BD7E2EC26] PRIMARY KEY ([id_produto], [id_requisicao]),
    CONSTRAINT [FK__ProdutoRe__id_pr__4F7CD00D] FOREIGN KEY ([id_produto]) REFERENCES [Produto] ([id]) ON DELETE CASCADE,
    CONSTRAINT [FK__ProdutoRe__id_re__4316F928] FOREIGN KEY ([id_requisicao]) REFERENCES [Requisicao] ([id]) ON DELETE CASCADE
);
GO

CREATE TABLE [StoreProdutos] (
    [Id] int NOT NULL IDENTITY,
    [Store] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Link] nvarchar(max) NOT NULL,
    [ProdutoScraperModelId] int NOT NULL,
    CONSTRAINT [PK_StoreProdutos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_StoreProdutos_ProdutoScraper_ProdutoScraperModelId] FOREIGN KEY ([ProdutoScraperModelId]) REFERENCES [ProdutoScraper] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Benchmarkings_IdProduto] ON [Benchmarkings] ([IdProduto]);
GO

CREATE INDEX [IX_Motivo_id_categoriamotivo] ON [Motivo] ([id_categoriamotivo]);
GO

CREATE INDEX [IX_ProdutoRequisicao_id_requisicao] ON [ProdutoRequisicao] ([id_requisicao]);
GO

CREATE UNIQUE INDEX [IX_ProdutoScraper_IdProduto] ON [ProdutoScraper] ([IdProduto]);
GO

CREATE INDEX [IX_Requisicao_id_departamento] ON [Requisicao] ([id_departamento]);
GO

CREATE INDEX [IX_Requisicao_id_funcionario] ON [Requisicao] ([id_funcionario]);
GO

CREATE INDEX [IX_StoreProdutos_ProdutoScraperModelId] ON [StoreProdutos] ([ProdutoScraperModelId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240311152332_DatabaseV1', N'6.0.27');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240311152741_DatabaseV2ProdutoUpdate', N'6.0.27');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Benchmarkings] ADD [StatusEmail] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240311193016_AddStatusEmal', N'6.0.27');
GO

COMMIT;
GO

