using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using TraktorMapping.TSI.Format;

namespace Editor.ViewModels
{
    public class MappingModel : INotifyPropertyChanged
    {
        public MappingModel(int id, string midiNote, string traktorCommand, TargetType targetType, string comment, MappingTargetDeck deck, MappingType type,
            MappingControllerType controllerType, MappingInteractionMode interactionMode, bool autoRepeat, bool invert, bool softTakeOver, float rotarySensitivity,
            float rotaryAcceleration, int ledInvert, MappingResolution resolution)
        {
            this.id = id;
            this.midiNote = midiNote;
            this.traktorCommand = traktorCommand;
            this.targetType = targetType;
            this.comment = comment;
            this.deck = deck;
            this.type = type;
            this.controllerType = controllerType;
            this.interactionMode = interactionMode;
            this.autoRepeat = autoRepeat;
            this.invert = invert;
            this.softTakeOver = softTakeOver;
            this.rotarySensitivity = rotarySensitivity;
            this.rotaryAcceleration = rotaryAcceleration;
            this.ledInvert = ledInvert;
            this.resolution = resolution;
        }

        private int id;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string midiNote;

        public string MidiNote
        {
            get { return midiNote; }
            set
            {
                midiNote = value;
                OnPropertyChanged(nameof(MidiNote));
            }
        }

        private string traktorCommand;

        public string TraktorCommand
        {
            get { return traktorCommand; }
            set
            {
                traktorCommand = value;
                OnPropertyChanged(nameof(TraktorCommand));
            }
        }

        private TargetType targetType;

        public TargetType TargetType
        {
            get { return targetType; }
            set
            {
                targetType = value;
                OnPropertyChanged(nameof(TargetType));
            }
        }

        private string comment;

        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        private MappingTargetDeck deck;

        public MappingTargetDeck Deck
        {
            get { return deck; }
            set
            {
                deck = value;
                OnPropertyChanged(nameof(Deck));
            }
        }

        private MappingType type;

        public MappingType Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        private MappingControllerType controllerType;

        public MappingControllerType ControllerType
        {
            get { return controllerType; }
            set
            {
                controllerType = value;
                OnPropertyChanged(nameof(ControllerType));
            }
        }

        private MappingInteractionMode interactionMode;

        public MappingInteractionMode InteractionMode
        {
            get { return interactionMode; }
            set
            {
                interactionMode = value;
                OnPropertyChanged(nameof(InteractionMode));
            }
        }

        private bool autoRepeat;

        public bool AutoRepeat
        {
            get { return autoRepeat; }
            set
            {
                autoRepeat = value;
                OnPropertyChanged(nameof(AutoRepeat));
            }
        }

        private bool invert;

        public bool Invert
        {
            get { return invert; }
            set
            {
                invert = value;
                OnPropertyChanged(nameof(Invert));
            }
        }

        private bool softTakeOver;

        public bool SoftTakeover
        {
            get { return softTakeOver; }
            set
            {
                softTakeOver = value;
                OnPropertyChanged(nameof(SoftTakeover));
            }
        }

        private float rotarySensitivity;

        public float RotarySensitivity
        {
            get { return rotarySensitivity; }
            set
            {
                rotarySensitivity = value;
                OnPropertyChanged(nameof(RotarySensitivity));
            }
        }

        private float rotaryAcceleration;

        public float RotaryAcceleration
        {
            get { return rotaryAcceleration; }
            set
            {
                rotaryAcceleration = value;
                OnPropertyChanged(nameof(RotaryAcceleration));
            }
        }

        private int ledInvert;

        public int LedInvert
        {
            get { return ledInvert; }
            set
            {
                ledInvert = value;
                OnPropertyChanged(nameof(LedInvert));
            }
        }

        private MappingResolution resolution;

        public MappingResolution Resolution
        {
            get { return resolution; }
            set
            {
                resolution = value;
                OnPropertyChanged(nameof(Resolution));
            }
        }

        public bool HasChanges { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            HasChanges = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
