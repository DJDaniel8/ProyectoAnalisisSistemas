using MultiMarcasAPP.Models;
using MultiMarcasAPP.Services.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MultiMarcasAPP.ViewModels.ProductosViewModels
{
    public class ProductoInformacionViewModel : ViewModelBase
    {
        public ProductoInformacionViewModel()
        {
            _Producto = new();
            _categorias = new();
            _proveedores = new();
        }

        public ProductoInformacionViewModel(ProductoViewModel producto, ICommand cargarImagenCommand)
        {
            _Producto = producto;
            _categorias = new();
            _proveedores = new();
        }

        private ObservableCollection<Proveedor> _proveedores;
        public IEnumerable<Proveedor> Proveedores => _proveedores;

        private ObservableCollection<Categoria> _categorias;
        public IEnumerable<Categoria> Categorias => _categorias;

        public ICommand CargarImagenCommand { get; set; }
        public ICommand QuitarImagenCommand { get; set; }
        public ICommand AgregarCategoriaCommand { get; set; }
        public ICommand ConfirmarAgregarCategoriaCommand { get; set; }
        public ICommand QuitarCategoriaCommand { get; set; }
        public ICommand AgregarProveedorCommand { get; set; }
        public ICommand ConfirmarAgregarProveedorCommand { get; set; }
        public ICommand QuitarProveedorCommand { get; set; }

        private ProductoViewModel _Producto;
        public ProductoViewModel Producto
        {
            get
            {
                return _Producto;
            }
            set
            {
                _Producto = value;
                OnPropertyChanged(nameof(Producto));
            }
        }

        private bool _TxtBoxReadOnly = true;
        public bool TxtBoxReadOnly
        {
            get
            {
                return _TxtBoxReadOnly;
            }
            set
            {
                _TxtBoxReadOnly = value;
                OnPropertyChanged(nameof(TxtBoxReadOnly));
            }
        }

        private Visibility _TxtBoxVisibility = Visibility.Visible;
        public Visibility TxtBoxVisibility
        {
            get
            {
                return _TxtBoxVisibility;
            }
            set
            {
                _TxtBoxVisibility = value;
                OnPropertyChanged(nameof(TxtBoxVisibility));
            }
        }

        private Visibility _ComboBoxVisibility = Visibility.Collapsed;
        public Visibility ComboBoxVisibility
        {
            get
            {
                return _ComboBoxVisibility;
            }
            set
            {
                _ComboBoxVisibility = value;
                OnPropertyChanged(nameof(ComboBoxVisibility));
            }
        }

        private Visibility _AddRemoveCategoryButtonsVisibility = Visibility.Collapsed;
        public Visibility AddRemoveCategoryButtonsVisibility
        {
            get
            {
                return _AddRemoveCategoryButtonsVisibility;
            }
            set
            {
                _AddRemoveCategoryButtonsVisibility = value;
                OnPropertyChanged(nameof(AddRemoveCategoryButtonsVisibility));
            }
        }

        private Visibility _AddRemoveProviderButtonsVisibility = Visibility.Collapsed;
        public Visibility AddRemoveProviderButtonsVisibility
        {
            get
            {
                return _AddRemoveProviderButtonsVisibility;
            }
            set
            {
                _AddRemoveProviderButtonsVisibility = value;
                OnPropertyChanged(nameof(AddRemoveProviderButtonsVisibility));
            }
        }

        private Visibility _ConfirmSaveCategory = Visibility.Collapsed;
        public Visibility ConfirmSaveCategory
        {
            get
            {
                return _ConfirmSaveCategory;
            }
            set
            {
                _ConfirmSaveCategory = value;
                OnPropertyChanged(nameof(ConfirmSaveCategory));
            }
        }

        private Visibility _ConfirmSaveProvider = Visibility.Collapsed;
        public Visibility ConfirmSaveProvider
        {
            get
            {
                return _ConfirmSaveProvider;
            }
            set
            {
                _ConfirmSaveProvider = value;
                OnPropertyChanged(nameof(ConfirmSaveProvider));
            }
        }

        private Visibility _EsDerivadoControlsVisibility = Visibility.Collapsed;
        public Visibility EsDerivadoControlsVisibility
        {
            get
            {
                return _EsDerivadoControlsVisibility;
            }
            set
            {
                _EsDerivadoControlsVisibility = value;
                OnPropertyChanged(nameof(EsDerivadoControlsVisibility));
            }
        }

        public void CargarCategorias()
        {
            _categorias.Clear();
            foreach (var item in CategoriaDAO.Get())
            {
                _categorias.Add(new(item.Item1, item.Item2));
            }
        }

        public void CargarProveedores()
        {
            _proveedores.Clear();
            foreach (var item in ProveedorDAO.Get())
            {
                _proveedores.Add(item);
            }
        }

        public void CargarCategoriasById(int id)
        {
            _categorias.Clear();
            foreach (var item in CategoriaDAO.Get(id) )
            {
                _categorias.Add(new(item.Item1, item.Item2));
            }

        }

        public void CargarProveedoresById(int id)
        {
            _proveedores.Clear();
            foreach (var item in ProveedorDAO.Get(id))
            {
                _proveedores.Add(item);
            }
        }

        public void CargarCategoriasByIdSinRelacion(int id)
        {
            _categorias.Clear();
            foreach (var item in CategoriaDAO.GetWithoutRelashionship(id))
            {
                _categorias.Add(new(item.Item1, item.Item2));
            }
        }

        public void CargarProveedoresByIdSinRelacion(int id)
        {
            _proveedores.Clear();
            foreach (var item in ProveedorDAO.GetWithoutRelashionship(id))
            {
                _proveedores.Add(item);
            }
        }

        private BitmapImage? _Imagen;
        public BitmapImage? Imagen
        {
            get { return _Imagen; }
            set
            {
                _Imagen = value;
                OnPropertyChanged(nameof(Imagen));
            }
        }

        private byte[]? _ImagenData;
        public byte[]? ImagenData
        {
            get { return _ImagenData; }
            set
            {
                _ImagenData = value;
                OnPropertyChanged(nameof(ImagenData));
            }
        }

        private string _CodigoPadre;
        public string CodigoPadre
        {
            get
            {
                return _CodigoPadre;
            }
            set
            {
                _CodigoPadre = value;
                OnPropertyChanged(nameof(CodigoPadre));
            }
        }

        private bool _EsDerivado;
        public bool EsDerivado
        {
            get
            {
                return _EsDerivado;
            }
            set
            {
                _EsDerivado = value;
                Producto.Producto.EsDerivado = value;
                OnPropertyChanged(nameof(EsDerivado));
            }
        }

        private Categoria _SelectedCategoria;
        public Categoria SelectedCategoria
        {
            get
            {
                return _SelectedCategoria;
            }
            set
            {
                _SelectedCategoria = value;
                OnPropertyChanged(nameof(SelectedCategoria));
            }
        }

        private Proveedor _SelectedProveedor;
        public Proveedor SelectedProveedor
        {
            get
            {
                return _SelectedProveedor;
            }
            set
            {
                _SelectedProveedor = value;
                OnPropertyChanged(nameof(SelectedProveedor));
            }
        }

        public int HowManyProviders()
        {
            return _proveedores.Count();
        }
    }
}
