// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: MarketService.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Axiverse.Services.Proto {

  /// <summary>Holder for reflection information generated from MarketService.proto</summary>
  public static partial class MarketServiceReflection {

    #region Descriptor
    /// <summary>File descriptor for MarketService.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static MarketServiceReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChNNYXJrZXRTZXJ2aWNlLnByb3RvImMKEVBsYWNlT3JkZXJSZXF1ZXN0EhgK",
            "BHR5cGUYASABKA4yCi5PcmRlclR5cGUSEwoLYXNzZXRfY2xhc3MYAiABKAkS",
            "EAoIcXVhbnRpdHkYAyABKAMSDQoFcHJpY2UYBCABKAMiFAoSUGxhY2VPcmRl",
            "clJlc3BvbnNlKkAKCU9yZGVyVHlwZRILCgdVTktOT1dOEAASBwoDQlVZEAES",
            "CAoEU0VMTBACEgkKBUxJTUlUEAMSCAoEU1RPUBAEMkYKDU1hcmtldFNlcnZp",
            "Y2USNQoKUGxhY2VPcmRlchISLlBsYWNlT3JkZXJSZXF1ZXN0GhMuUGxhY2VP",
            "cmRlclJlc3BvbnNlQhqqAhdBeGl2ZXJzZS5TZXJ2aWNlcy5Qcm90b2IGcHJv",
            "dG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Axiverse.Services.Proto.OrderType), }, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Axiverse.Services.Proto.PlaceOrderRequest), global::Axiverse.Services.Proto.PlaceOrderRequest.Parser, new[]{ "Type", "AssetClass", "Quantity", "Price" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Axiverse.Services.Proto.PlaceOrderResponse), global::Axiverse.Services.Proto.PlaceOrderResponse.Parser, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum OrderType {
    [pbr::OriginalName("UNKNOWN")] Unknown = 0,
    [pbr::OriginalName("BUY")] Buy = 1,
    [pbr::OriginalName("SELL")] Sell = 2,
    [pbr::OriginalName("LIMIT")] Limit = 3,
    [pbr::OriginalName("STOP")] Stop = 4,
  }

  #endregion

  #region Messages
  public sealed partial class PlaceOrderRequest : pb::IMessage<PlaceOrderRequest> {
    private static readonly pb::MessageParser<PlaceOrderRequest> _parser = new pb::MessageParser<PlaceOrderRequest>(() => new PlaceOrderRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<PlaceOrderRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Axiverse.Services.Proto.MarketServiceReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlaceOrderRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlaceOrderRequest(PlaceOrderRequest other) : this() {
      type_ = other.type_;
      assetClass_ = other.assetClass_;
      quantity_ = other.quantity_;
      price_ = other.price_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlaceOrderRequest Clone() {
      return new PlaceOrderRequest(this);
    }

    /// <summary>Field number for the "type" field.</summary>
    public const int TypeFieldNumber = 1;
    private global::Axiverse.Services.Proto.OrderType type_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Axiverse.Services.Proto.OrderType Type {
      get { return type_; }
      set {
        type_ = value;
      }
    }

    /// <summary>Field number for the "asset_class" field.</summary>
    public const int AssetClassFieldNumber = 2;
    private string assetClass_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string AssetClass {
      get { return assetClass_; }
      set {
        assetClass_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "quantity" field.</summary>
    public const int QuantityFieldNumber = 3;
    private long quantity_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long Quantity {
      get { return quantity_; }
      set {
        quantity_ = value;
      }
    }

    /// <summary>Field number for the "price" field.</summary>
    public const int PriceFieldNumber = 4;
    private long price_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long Price {
      get { return price_; }
      set {
        price_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as PlaceOrderRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(PlaceOrderRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Type != other.Type) return false;
      if (AssetClass != other.AssetClass) return false;
      if (Quantity != other.Quantity) return false;
      if (Price != other.Price) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Type != 0) hash ^= Type.GetHashCode();
      if (AssetClass.Length != 0) hash ^= AssetClass.GetHashCode();
      if (Quantity != 0L) hash ^= Quantity.GetHashCode();
      if (Price != 0L) hash ^= Price.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Type != 0) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Type);
      }
      if (AssetClass.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(AssetClass);
      }
      if (Quantity != 0L) {
        output.WriteRawTag(24);
        output.WriteInt64(Quantity);
      }
      if (Price != 0L) {
        output.WriteRawTag(32);
        output.WriteInt64(Price);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Type != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Type);
      }
      if (AssetClass.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(AssetClass);
      }
      if (Quantity != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(Quantity);
      }
      if (Price != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(Price);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(PlaceOrderRequest other) {
      if (other == null) {
        return;
      }
      if (other.Type != 0) {
        Type = other.Type;
      }
      if (other.AssetClass.Length != 0) {
        AssetClass = other.AssetClass;
      }
      if (other.Quantity != 0L) {
        Quantity = other.Quantity;
      }
      if (other.Price != 0L) {
        Price = other.Price;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            type_ = (global::Axiverse.Services.Proto.OrderType) input.ReadEnum();
            break;
          }
          case 18: {
            AssetClass = input.ReadString();
            break;
          }
          case 24: {
            Quantity = input.ReadInt64();
            break;
          }
          case 32: {
            Price = input.ReadInt64();
            break;
          }
        }
      }
    }

  }

  public sealed partial class PlaceOrderResponse : pb::IMessage<PlaceOrderResponse> {
    private static readonly pb::MessageParser<PlaceOrderResponse> _parser = new pb::MessageParser<PlaceOrderResponse>(() => new PlaceOrderResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<PlaceOrderResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Axiverse.Services.Proto.MarketServiceReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlaceOrderResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlaceOrderResponse(PlaceOrderResponse other) : this() {
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PlaceOrderResponse Clone() {
      return new PlaceOrderResponse(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as PlaceOrderResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(PlaceOrderResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(PlaceOrderResponse other) {
      if (other == null) {
        return;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
