using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;
using System.Web.Services.Description;

namespace SiLA.Provider
{
    /// <summary>
    /// Attribute which injects wsdl:documentation elements into the WSDL.
    /// </summary>
    /// <remarks>
    /// This attribute can be applied to the service contract and it's methods.  
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
    public class WsdlDocumentationAttribute: Attribute, IContractBehavior, IOperationBehavior, IWsdlExportExtension
    {
        private ContractDescription _contractDescription;
        private OperationDescription _operationDescription;

        /// <summary>
        /// Initializes a new instance of WsdlDocumentationAttribute.
        /// </summary>
        /// <param name="text">Text to inject into the WSDL.</param>
        public WsdlDocumentationAttribute(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// Text to inject into the WSDL for the target element.
        /// </summary>
        public string Text { get; set; }

        void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
        {
            // This is either for a service contract or operation, so set documentation accordingly.
            if (_contractDescription != null)
            {
                // Attribute was applied to a contract.
                context.WsdlPortType.Documentation = this.Text;
            }
            else
            {
                // Attribute was applied to an operation.
                Operation operation = context.GetOperation(_operationDescription);
                if (operation != null)
                {
                    operation.Documentation = this.Text;
                }
            }
        }

        void IContractBehavior.ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            _contractDescription = contractDescription;
        }

        void IOperationBehavior.ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            _operationDescription = operationDescription;
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            //throw new NotImplementedException();
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
           // throw new NotImplementedException();
        }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
          //  throw new NotImplementedException();
        }

        public void Validate(OperationDescription operationDescription)
        {
            //throw new NotImplementedException();
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
           // throw new NotImplementedException();
        }

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
           // throw new NotImplementedException();
        }

        public void ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
        {
           // throw new NotImplementedException();
        }
    }
}