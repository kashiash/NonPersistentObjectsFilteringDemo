using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace NonPersistentObjectsDemo.Module.BusinessObjects {

    public abstract class BoundNonPersistentObjectBase : NonPersistentObjectBase, IObjectSpaceLink {
        private IObjectSpace _ObjectSpace;
        protected IObjectSpace ObjectSpace { get { return _ObjectSpace; } }
        IObjectSpace IObjectSpaceLink.ObjectSpace {
            get { return _ObjectSpace; }
            set {
                if(_ObjectSpace != value) {
                    OnObjectSpaceChanging();
                    _ObjectSpace = value;
                    OnObjectSpaceChanged();
                }
            }
        }
        protected virtual void OnObjectSpaceChanging() { }
        protected virtual void OnObjectSpaceChanged() { }
        protected IObjectSpace FindPersistentObjectSpace(Type type) {
            return ((NonPersistentObjectSpace)ObjectSpace).AdditionalObjectSpaces.FirstOrDefault(os => os.IsKnownType(type));
        }
    }

    public abstract class NonPersistentObjectBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void SetPropertyValue<T>(string name, ref T field, T value) {
            if(!Equals(field, value)) {
                field = value;
                OnPropertyChanged(name);
            }
        }
        [Browsable(false)]
        public object This { get { return this; } }
    }
}
