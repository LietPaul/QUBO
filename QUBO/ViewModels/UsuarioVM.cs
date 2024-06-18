namespace QUBO.ViewModels
{
    public class UsuarioVM
    {
        public string NombreUsuario { get; set; } = null!;

        public string Contrasenia { get; set; } = null!;

        public string? RolUsuario { get; set; }

        public string? ConfimarContrasenia { get; set; }
    }
}
