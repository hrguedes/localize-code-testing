INSERT INTO Sistema.Usuarios (Id, Nome, Email, Senha, RegistroAtivo, RegistroCriado, UltimaAtualizacao, RegistroRemovido)
VALUES
    (NEWID(), 'Alice Silva', 'alice.silva@example.com', 'Senha123', 1, GETDATE(), NULL, NULL),
    (NEWID(), 'Bruno Costa', 'bruno.costa@example.com', 'Senha123', 1, GETDATE(), NULL, NULL),
    (NEWID(), 'Carla Souza', 'carla.souza@example.com', 'Senha123', 1, GETDATE(), NULL, NULL),
    (NEWID(), 'Daniel Oliveira', 'daniel.oliveira@example.com', 'Senha123', 1, GETDATE(), NULL, NULL),
    (NEWID(), 'Eduarda Pereira', 'eduarda.pereira@example.com', 'Senha123', 1, GETDATE(), NULL, NULL),
    (NEWID(), 'Fernando Lima', 'fernando.lima@example.com', 'Senha123', 1, GETDATE(), NULL, NULL),
    (NEWID(), 'Gabriela Martins', 'gabriela.martins@example.com', 'Senha123', 1, GETDATE(), NULL, NULL),
    (NEWID(), 'Henrique Barbosa', 'henrique.barbosa@example.com', 'Senha123', 1, GETDATE(), NULL, NULL),
    (NEWID(), 'Isabela Rocha', 'isabela.rocha@example.com', 'Senha123', 1, GETDATE(), NULL, NULL),
    (NEWID(), 'João Mendes', 'joao.mendes@example.com', 'Senha123', 1, GETDATE(), NULL, NULL);
GO
